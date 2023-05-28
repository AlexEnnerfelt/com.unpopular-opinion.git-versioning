/* MIT License
Copyright (c) 2016 RedBlueGames
*/

using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;
using UnityEditor.Build;
using System;


namespace Ennerfelt.GitVersioning
{
    public class BuildVersion : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;
        private string _previousBuildVersion;

        public static bool AreVersionsMatching() {
            var v = PlayerSettings.bundleVersion;
            var current = v.Substring(0, v.LastIndexOf("-"));
            var git = Git.BuildVersion;
            return (git == current);
        }
        public static int? GetBuildNumber() {
            var v = PlayerSettings.bundleVersion;
            try {
                var b = v.Substring(v.LastIndexOf("b") + 1);
                int result = Int32.Parse(b);
                return result;
            }
            catch (Exception) {
                return null;
            }

        }

        public void OnPostprocessBuild(BuildReport report) {
            Debug.Log(PlayerSettings.bundleVersion);
            Debug.Log("Successful build");
        }

        public void OnPreprocessBuild(BuildReport report) {
            var build = GetBuildNumber();
            if (build.HasValue) {
                if (AreVersionsMatching()) {
                    build = 0;
                }
                else {
                    build++;
                }
                PlayerSettings.bundleVersion = $"{Git.BuildVersion} {GetOS()}-b{build}";
            }
            else {
                PlayerSettings.bundleVersion = $"{Git.BuildVersion} {GetOS()}";
            }
        }
        private string GetOS() {
            var os = EditorUserBuildSettings.activeBuildTarget;

            switch (os) {
                case BuildTarget.StandaloneOSX:
                    return "OSX";
                case BuildTarget.StandaloneWindows:
                    return "Win-32";
                case BuildTarget.StandaloneWindows64:
                    return "Win-64";
                case BuildTarget.WSAPlayer:
                    return "Wsa";
                case BuildTarget.StandaloneLinux64:
                    return "Linux";
                case BuildTarget.PS4:
                    return "PS4";
                case BuildTarget.XboxOne:
                    return "XboxOne";
                case BuildTarget.Switch:
                    return "Switch";
                case BuildTarget.GameCoreXboxOne:
                    return "GCXboxOne";
                default:
                    return string.Empty;
            }
        }
    }
}