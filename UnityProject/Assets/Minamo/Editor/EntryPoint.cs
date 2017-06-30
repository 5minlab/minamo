using System.IO;
using UnityEngine;

namespace Assets.Minamo.Editor {

    /// <summary>
    /// cli entry point
    /// </summary>
    public class EntryPoint {
        public static void Build() {
            string configFilePath;
            if (!EnvironmentReader.TryRead("CONFIG_PATH", out configFilePath)) {
                Debug.Log("cannot find CONFIG_PATH");
                return;
            }

            string outputFilePath;
            if (!EnvironmentReader.TryRead("OUTPUT_PATH", out outputFilePath)) {
                Debug.Log("cannot find OUTPUT_PATH");
                return;
            }

            BuildCommon(configFilePath, outputFilePath);
        }

        internal static void BuildCommon(string configFilePath, string outputFilePath) {
            var content = File.ReadAllText(configFilePath);
            var config = new Config(content);

            var currModifiers = config.CreateCurrentModifiers();
            var nextModifiers = config.CreateCurrentModifiers();

            foreach (var m in nextModifiers) {
                var tokens = m.GetType().ToString().Split('.');
                var name = tokens[tokens.Length - 1];
                Debug.LogFormat("[MinamoLog] {0}: {1}", name, m.GetConfigText());
                m.Apply();
            }

            var executor = config.PlayerBuild;
            Debug.LogFormat("[MinamoLog] {0}: {1}", "PlayerBuildExecutor", executor.GetConfigText());
            executor.Build(outputFilePath);

            // restore
            foreach (var m in currModifiers) {
                m.Apply();
            }
        }

        public static void ExportPackage() {
            string output;
            if (EnvironmentReader.TryRead("EXPORT_PATH", out output)) {
                Debug.LogFormat("export package : {0}", output);
                var b = new PackageBuilder();
                b.Build(output);
            }
        }
    }
}
