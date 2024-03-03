/* MIT License
Copyright (c) 2016 RedBlueGames
*/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;


namespace UnpopularOpinion.GitVersioning {
	public class BuildVersion {
		public static string[] excludeBranches = new string[] {
			"develop","main","master", "HEAD"
		};

		public static string GetSemanticVersion() {
			var version = Git.BuildVersion;
			return version;
		}

		public static string GetBuildVersion(bool includeTarget = false, bool includeBranch = false, bool showAllBranches = false) {
			var version = GetSemanticVersion();

			if (includeBranch) {
				var branch = Git.Branch;
				if (excludeBranches.Contains(branch)) {
					if (showAllBranches) {
						version += $" - {branch}";
					}
				} else {
					version += $" - {branch}";
				}
			}

			if (includeTarget) {
				version += $" - {TargetVersionCode[EditorUserBuildSettings.activeBuildTarget]}";
			}

			return version;
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