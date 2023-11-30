/* MIT License
Copyright (c) 2016 RedBlueGames
*/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;


namespace Ennerfelt.GitVersioning {
	public class BuildVersion {
		public int callbackOrder => 0;
		public static string[] excludeBranches = new string[] {
			"develop","main","master"
		};


		public static string GetBuildVersion(BuildTarget target, bool includeBranch = false) {
			var version = Git.BuildVersion;
			version += $"-{TargetVersionCode[target]}";

			var branch = Git.Branch;
			if (includeBranch && !excludeBranches.Contains(branch)) {
				version += $" : {branch}";
			}
			return version;
		}


		public static bool AreVersionsMatching() {
			var v = PlayerSettings.bundleVersion;
			var current = v.Substring(0, v.LastIndexOf("-"));
			var git = Git.BuildVersion;
			return git == current;
		}
		public static int? GetBuildNumber() {
			var v = PlayerSettings.bundleVersion;
			try {
				var b = v.Substring(v.LastIndexOf("b") + 1);
				var result = Int32.Parse(b);
				return result;
			} catch (Exception) {
				return null;
			}
		}

		private static readonly Dictionary<BuildTarget, string> TargetVersionCode = new() {
			{BuildTarget.StandaloneOSX, "OSX" },
			{BuildTarget.StandaloneWindows, "Win-32" },
			{BuildTarget.StandaloneWindows64, "Win-64" },
			{BuildTarget.StandaloneLinux64, "Linux" },
			{BuildTarget.PS4, "PS4" },
			{BuildTarget.XboxOne, "XboxOne" },
			{BuildTarget.Switch, "Switch" },
			{BuildTarget.GameCoreXboxOne, "GCXboxOne" },
		};

	}
}