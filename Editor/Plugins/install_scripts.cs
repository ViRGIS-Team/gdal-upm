using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;
using OSGeo.GDAL;

#if UNITY_EDITOR
namespace OSGeo.Install {

    public class Install{

        const string packageVersion = "1.0.0";

        [InitializeOnLoadMethod]
        static void OnProjectLoadedinEditor()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            EditorUtility.DisplayProgressBar("Restoring Conda Package", "GDAL", 0);
             
            if (Application.isEditor) {
                try
                {
                    string currentVersion = Gdal.VersionInfo(null);
                }
                catch (Exception e)
                {
                    Debug.Log($"Error in Conda Package GDAL: {e.ToString()}");
                    UpdatePackage();
                    AssetDatabase.Refresh();
                };
            };

            EditorUtility.ClearProgressBar();
            stopwatch.Stop();
            Debug.Log($"Gdal refresh took {stopwatch.Elapsed.TotalSeconds} seconds");
        }


        static void UpdatePackage()
        {
            Debug.Log("Mdal Install Script Awake");
            string path = Path.GetDirectoryName(new StackTrace(true).GetFrame(0).GetFileName());
#if UNITY_EDITOR_WIN
            path = Path.Combine(path, "install_script.ps1");
#else
            path = Path.Combine(path, "install_script.sh");
#endif
            string response = Conda.Conda.Install($"gdal-csharp={packageVersion}", path);
        }
    }
}
#endif
