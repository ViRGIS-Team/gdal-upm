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
                        conda.Add($"gdal-csharp={packageVersion}", new ConfigFile.Package()
                        {
                            Name = "gdal",
                            Cleans = new ConfigFile.Clean[] {
                                new()
                                {
                                    path = new string[] {"conda_bin" },
                                    excludes = new string[] { "." }, 
                                    includes = new string[] { "^GDAL." }
                                },
                                new()
                                {
                                    path = new string[] {"conda_shared", "gdal" },
                                    excludes = new string[] { "." },
                                    includes = new string[] { "./.nupkg$" }
                                }
                            },
                            Shared_Datas = new string[] {
                                "gdal", "proj"
                            }
                        });
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
