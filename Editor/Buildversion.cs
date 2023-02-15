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


        public static bool AreVersionsMatching() {
            var v = PlayerSettings.bundleVersion;
            var current = v.Substring(0, v.LastIndexOf("-"));
            var git = Git.BuildVersion;
            return (git == current);
        }
        public static int? GetBuildNumber() {
            var v = PlayerSettings.bundleVersion;
            var b = v.Substring(v.LastIndexOf("b") + 1);
            try {
                int result = Int32.Parse(b);
                return result;
            } catch (Exception) {
                return null;
            }

        }

        public void OnPostprocessBuild(BuildReport report) {
            Debug.Log($"{PlayerSettings.bundleVersion} Sucessfully built");
            if (report.summary.result == BuildResult.Succeeded) {
            }
        }

        public void OnPreprocessBuild(BuildReport report) {
            var build = GetBuildNumber();
            if (AreVersionsMatching()) {
                build++;
            } else {
                build = 0;
            }

            // This gets the Build Version from Git via the `git describe` command
            PlayerSettings.bundleVersion = Git.BuildVersion + "-b" + build;
        }
    } 
}