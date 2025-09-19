using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;
using System;
using System.Linq;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using OSGeo.GDAL;
using System.Text.RegularExpressions;

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
                    try
                    {
                        List<Conda.CondaItem> list = Conda.Conda.Info().Items.ToList();
                        Conda.CondaItem entry = list.Find(item => item.name == "gdal-csharp");
                        if (entry == null || entry.version != packageVersion)
                        {
                            response = UpdatePackage();
                        }
                        string currentVersion = Gdal.VersionInfo(null);
                    }
                    catch (Exception e)
                    {
                        Debug.Log($"Error in Conda Package GDAL: {e?.ToString()}");
                        response = UpdatePackage();
                    };
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
            string condaLibrary;
            string condaShared;
#if UNITY_EDITOR_WIN
            condaLibrary = Path.Combine(Application.dataPath, "Conda", "Env", "Library");
            condaShared = Path.Combine(condaLibrary, "share");
#endif
            try
            {
                Conda.Conda.RecurseAndClean(Path.Combine(condaLibrary, "bin"),
                    new Regex[] {
                        new Regex("."),
                    },
                    new Regex[] {
                        new Regex("^GDAL"),
                        new Regex("_csharp.dll^"),
                    });
                string sharedAssets = Application.streamingAssetsPath;
                if (!Directory.Exists(sharedAssets)) Directory.CreateDirectory(sharedAssets);
                string gdalDir = Path.Combine(sharedAssets, "gdal");
                if (!Directory.Exists(gdalDir)) Directory.CreateDirectory(gdalDir);
                string projDir = Path.Combine(sharedAssets, "proj");
                if (!Directory.Exists(projDir)) Directory.CreateDirectory(projDir);

                foreach (var file in Directory.GetFiles(Path.Combine(condaShared, "gdal")))
                {
                    File.Copy(file, Path.Combine(gdalDir, Path.GetFileName(file)), true);
                }

                foreach (var file in Directory.GetFiles(Path.Combine(condaShared, "proj")))
                {
                    File.Copy(file, Path.Combine(projDir, Path.GetFileName(file)), true);
                }
            }
            catch ( Exception e )
            {
                _ = e;
            }

            Conda.Conda.TreeShake();

            AssetDatabase.Refresh();

            return resp;

        }
    }
}
