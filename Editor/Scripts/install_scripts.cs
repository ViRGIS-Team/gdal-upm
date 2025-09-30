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
                Stopwatch stopwatch = new Stopwatch();
                string response = "";
                stopwatch.Start();
                EditorUtility.DisplayProgressBar("Restoring Conda Package", "GDAL", 0);

                if (Application.isEditor)
                {
                    if (!Conda.Conda.IsInstalled("gdal-csharp", packageVersion))
                    {
                        response = UpdatePackage();
                        AssetDatabase.Refresh();
                    }
                };

                EditorUtility.ClearProgressBar();
                stopwatch.Stop();
                Debug.Log($"GDAL refresh took {stopwatch.Elapsed.TotalSeconds} seconds" + response);
            }
            SessionState.SetBool("GdalInitDone", true);
        }


        static string UpdatePackage()
        {
            Debug.Log("Gdal Install Script Awake");
            string resp = Conda.Conda.Install($"gdal-csharp={packageVersion}");
            string condaGdal = Path.Combine(Conda.Conda.condaShared, "gdal");
            try
            {
                Conda.Conda.RecurseAndClean(Conda.Conda.condaBin,
                    new Regex[] {
                        new Regex("."),
                    },
                    new Regex[] {
                        new Regex("^GDAL.")
                    });
                Conda.Conda.RecurseAndClean(condaGdal,
                    new Regex[] {
                        new Regex("."),
                    },
                    new Regex[] {
                        new Regex("./.nupkg$"),
                    });
                string sharedAssets = Application.streamingAssetsPath;
                if (!Directory.Exists(sharedAssets)) Directory.CreateDirectory(sharedAssets);
                string gdalDir = Path.Combine(sharedAssets, "gdal");
                if (!Directory.Exists(gdalDir)) Directory.CreateDirectory(gdalDir);
                string projDir = Path.Combine(sharedAssets, "proj");
                if (!Directory.Exists(projDir)) Directory.CreateDirectory(projDir);

                foreach (var file in Directory.GetFiles(condaGdal))
                {
                    File.Copy(file, Path.Combine(gdalDir, Path.GetFileName(file)), true);
                }

                foreach (var file in Directory.GetFiles(Path.Combine(Conda.Conda.condaShared, "proj")))
                {
                    File.Copy(file, Path.Combine(projDir, Path.GetFileName(file)), true);
                }
            }
            catch (Exception e)
            {
                _ = e;
            }

            Conda.Conda.TreeShake();

            AssetDatabase.Refresh();

            return resp;

        }
    }
}
