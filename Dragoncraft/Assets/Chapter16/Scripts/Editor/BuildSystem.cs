#if UNITY_EDITOR
using UnityEditor.Build.Reporting;
using UnityEditor;
using UnityEngine;

namespace Dragoncraft
{
    public static class BuildSystem
    {
        private static string[] _releaseScenes = new[] {
            "Assets/Chapter16/Scenes/Level01.unity",
            "Assets/Chapter16/Scenes/GameUI.unity"
        };

        private static string[] _debugScenes = new[] {
            "Assets/Chapter16/Scenes/Playground.unity",
            "Assets/Chapter16/Scenes/GameUI.unity"
        };

        [MenuItem("Dragoncraft/Build/Build All (Debug and Release)", priority = 0)]
        private static void BuildAll()
        {
            BuildWindowsDebug();
            BuildWindowsRelease();
            BuildOSXDebug();
            BuildOSXRelease();
        }

        [MenuItem("Dragoncraft/Build/Build Windows (Debug)", priority = 1)]
        private static void BuildWindowsDebug()
        {
            string outputPath = "Builds/Windows/Debug/Dragoncraft.exe";
            Build(BuildTarget.StandaloneWindows, BuildOptions.Development, _debugScenes, outputPath);
        }

        [MenuItem("Dragoncraft/Build/Build Windows (Release)", priority = 2)]
        private static void BuildWindowsRelease()
        {
            string outputPath = "Builds/Windows/Release/Dragoncraft.exe";
            Build(BuildTarget.StandaloneWindows, BuildOptions.None, _releaseScenes, outputPath);
        }

        [MenuItem("Dragoncraft/Build/Build OSX (Debug)", priority = 3)]
        private static void BuildOSXDebug()
        {
            string outputPath = "Builds/OSX/Debug/Dragoncraft";
            Build(BuildTarget.StandaloneOSX, BuildOptions.Development, _debugScenes, outputPath);
        }

        [MenuItem("Dragoncraft/Build/Build OSX (Release)", priority = 4)]
        private static void BuildOSXRelease()
        {
            string outputPath = "Builds/OSX/Release/Dragoncraft";
            Build(BuildTarget.StandaloneOSX, BuildOptions.None, _releaseScenes, outputPath);
        }

        private static void Build(BuildTarget buildTarget, BuildOptions buildOptions, string[] scenes, string outputPath)
        {
            BuildPlayerOptions options = new BuildPlayerOptions();
            options.scenes = scenes;
            options.locationPathName = outputPath;
            options.target = buildTarget;
            options.options = buildOptions;

            BuildReport report = BuildPipeline.BuildPlayer(options);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log($"Build succeeded: {summary.outputPath}");
            }
            else
            {
                Debug.LogError($"Build failed: {summary.totalErrors}");
            }
        }
    }

}
#endif
