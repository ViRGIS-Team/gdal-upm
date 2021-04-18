using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;

#if UNITY_EDITOR
namespace OSGeo {

    public class Install{

#if UNITY_EDITOR_WIN
        const string test = "gdalinfo.exe";
#elif UNITY_EDITOR_OSX
        const string test = "gdalinfo";
#elif UNITY_EDITOR_LINUX
        const string test = "gdalinfo";
#endif

        const string packageVersion = "3.2.2";

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
#if UNITY_EDITOR_WIN
                    string file = Path.Combine(pluginPath, test);
#else
                    string file = Path.Combine(pluginPath, "bin", test);
#endif
                    if (!File.Exists(file))
                    {
                        UpdatePackage();
                        AssetDatabase.Refresh();
                    }
                    else if (!EditorApplication.isPlayingOrWillChangePlaymode)
                    {
                        string currentVersion = "0";
                        string response;
                        try
                        {
                            using (Process compiler = new Process())
                            {
                                compiler.StartInfo.FileName = file;
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
                        AssetDatabase.Refresh();
                    }
                }
                catch (Exception e)
                {
                    // do nothing
                    Debug.Log($"Error in Conda Package {test} : {e.ToString()}");
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
#if UNITY_EDITOR_WIN
                compiler.StartInfo.FileName = "powershell.exe";
                compiler.StartInfo.Arguments = $"-ExecutionPolicy Bypass \"{Path.Combine(path, "install_script.ps1")}\" -package gdal " +
                                                    $"-install {install} " +
                                                    $"-destination '{pluginPath}' " +
                                                    $"-test {test} " +
                                                    $"-shared_assets '{Application.streamingAssetsPath}' ";
#else
                compiler.StartInfo.FileName = "/bin/bash";
                compiler.StartInfo.Arguments = $" -i '{Path.Combine(path, "install_script.sh")}' " +
                                                $"-p gdal " +
                                                $"-i {install} " +
                                                $"-d '{pluginPath}' " +
                                                $"-t {test} " +
                                                $"-s '{Application.streamingAssetsPath}'  ";

#endif
                Debug.Log(compiler.StartInfo.Arguments);
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
#endif
