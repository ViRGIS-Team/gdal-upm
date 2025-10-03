using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;
using System.Text.RegularExpressions;
using Conda;

namespace OSGeo.Install {

    public class Install : AssetPostprocessor
    {

        const string packageVersion = "2.1.4";

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (!SessionState.GetBool("GdalInitDone", false))
            {
                Stopwatch stopwatch = new();
                string response = "";
                stopwatch.Start();
                EditorUtility.DisplayProgressBar("Restoring Conda Package", "GDAL", 0);

                if (Application.isEditor)
                {
                    CondaApp conda = new();
                    if (!conda.IsInstalled("gdal-csharp", packageVersion))
                    {
                        Debug.Log("Gdal Install Script Awake");
                        string resp =  conda.Install($"gdal-csharp={packageVersion}");
                        
                        string condaGdal = Path.Combine(conda.condaShared, "gdal");
                        try
                        {
                            conda.RecurseAndClean(conda.condaBin,
                                new Regex[] {
                                    new Regex("."),
                                    },
                                new Regex[] {
                                    new Regex("^GDAL.")
                                 }
                            );
                            conda.RecurseAndClean(condaGdal,
                                new Regex[] {
                                new Regex("."),
                                        },
                                new Regex[] {
                                    new Regex("./.nupkg$"),
                                         }
                            );
                            string sharedAssets = Application.streamingAssetsPath;
                            if (!Directory.Exists(sharedAssets)) Directory.CreateDirectory(sharedAssets);
                            string gdalDir = Path.Combine(sharedAssets, "gdal");
                            if (!Directory.Exists(gdalDir)) Directory.CreateDirectory(gdalDir);
                            string projDir = Path.Combine(sharedAssets, "proj");
                            if (!Directory.Exists(projDir)) Directory.CreateDirectory(projDir);

                            if (Directory.Exists(Path.Combine(conda.condaShared, "gdal")))
                                foreach (var file in Directory.GetFiles(Path.Combine(conda.condaShared, "gdal")))
                                {
                                    File.Copy(file, Path.Combine(gdalDir, Path.GetFileName(file)), true);
                                }

                            if (Directory.Exists(Path.Combine(conda.condaShared, "proj")))
                                foreach (var file in Directory.GetFiles(Path.Combine(conda.condaShared, "proj")))
                                {
                                    File.Copy(file, Path.Combine(projDir, Path.GetFileName(file)), true);
                                }
                        }
                        catch (Exception e)
                        {
                            Debug.LogException(e);
                        }
                        conda.TreeShake();
                        AssetDatabase.Refresh();
                    }
                };

                EditorUtility.ClearProgressBar();
                stopwatch.Stop();
                Debug.Log($"GDAL refresh took {stopwatch.Elapsed.TotalSeconds} seconds" + response);
            }
            SessionState.SetBool("GdalInitDone", true);
        }
    }
}
