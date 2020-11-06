using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;
using Gdal = OSGeo.GDAL.Gdal;

namespace OSGeo {

    public class Install{

        const string sharedObject = "gdalinfo.exe";
        const string packageVersion = "3.1.4";

        [InitializeOnLoadMethod]
        static void OnProjectLoadedinEditor()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            EditorUtility.DisplayProgressBar("Restoring Conda Package", "GDAL", 0);
             
            if (Application.isEditor) {
                try
                {
                    string pluginPath = Path.Combine(Application.dataPath, "Conda");
                    if (!Directory.Exists(pluginPath)) Directory.CreateDirectory(pluginPath);
                    string file = Path.Combine(pluginPath, sharedObject);
                    if (!File.Exists(file))
                    {
                        UpdatePackage();
                    }
                    else if (!EditorApplication.isPlayingOrWillChangePlaymode)
                    {
                        string currentVersion = "0";
                        string response;
                        try
                        {
                            using (Process compiler = new Process())
                            {
                                compiler.StartInfo.FileName = Path.Combine(pluginPath, "gdalinfo.exe");
                                compiler.StartInfo.Arguments = "--version";
                                compiler.StartInfo.UseShellExecute = false;
                                compiler.StartInfo.RedirectStandardOutput = true;
                                compiler.StartInfo.CreateNoWindow = true;
                                compiler.Start();

                                response = compiler.StandardOutput.ReadToEnd();

                                compiler.WaitForExit();
                            }
                            currentVersion = response.Split(new char[] { ' ', ',' })[1];
                            Debug.Log($"GDAL Version : {currentVersion}");
                        } catch (Exception e)
                        {
                            Debug.Log($"GDAL Version error {e.ToString()}");
                        }
                        if (currentVersion != packageVersion)
                        {
                            UpdatePackage();
                        }
                    }
                    AssetDatabase.Refresh();
                }
                catch (Exception e)
                {
                    // do nothing
                    Debug.Log($"Error in Conda Package {sharedObject} : {e.ToString()}");
                };
            };

            EditorUtility.ClearProgressBar();
            stopwatch.Stop();
            Debug.Log($"Gdal refresh took {stopwatch.Elapsed.TotalSeconds} seconds");
        }
        static void UpdatePackage() {
            Debug.Log("Gdal Install Script Awake"); 
            string pluginPath = Path.Combine(Application.dataPath, "Conda");
            string path = Path.GetDirectoryName(new StackTrace(true).GetFrame(0).GetFileName());
            string exec = Path.Combine(path, "install_script.ps1");
            string response;
            string install = $"gdal={packageVersion}";
            Debug.Log(Application.streamingAssetsPath);
            using (Process compiler = new Process())
            {
                compiler.StartInfo.FileName = "powershell.exe";
                compiler.StartInfo.Arguments = $"-ExecutionPolicy Bypass {exec} -package gdal " +
                                                    $"-install {install} " +
                                                    $"-destination {pluginPath} " +
                                                    $"-test gdalinfo.exe " +
                                                    $"-shared_assets {Application.streamingAssetsPath} ";
                compiler.StartInfo.UseShellExecute = false;
                compiler.StartInfo.RedirectStandardOutput = true;
                compiler.StartInfo.CreateNoWindow = true;
                compiler.Start();

                response = compiler.StandardOutput.ReadToEnd();

                compiler.WaitForExit();
            }
            Debug.Log(response);
        }
    }
}
