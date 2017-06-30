using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class PackageBuilder {
        public static void Export() {
            var b = new PackageBuilder();
            var assets = b.GetAssetPaths();

            string output;
            if (EnvironmentReader.TryRead("EXPORT_PATH", out output)) {
                Debug.LogFormat("export package : {0}", output);
                AssetDatabase.ExportPackage(assets, output);
            }
        }

        interface IAssetFinder {
            string[] GetList();
        }

        class MinamoAssetFinder : IAssetFinder {
            public string[] GetList() {
                var dirs = new string[] {
                    "Assets/Minamo"
                };
                var set = new HashSet<string>();
                var founds = AssetDatabase.FindAssets("", dirs);
                foreach (var guid in founds) {
                    var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    if (assetPath.EndsWith(".cs")) {
                        set.Add(assetPath);
                    }
                }

                var list = new List<string>(set);
                return list.ToArray();
            }
        }

        public string[] GetAssetPaths() {
            var finders = new IAssetFinder[] {
                new MinamoAssetFinder(),
            };

            var assetPaths = new List<string>();
            foreach (var f in finders) {
                assetPaths.AddRange(f.GetList());
            }
            return assetPaths.ToArray();
        }
    }

    class Menu_PackageExporter : ScriptableWizard {
        [SerializeField]
        string[] assets = null;

        [MenuItem("Window/Minamo/Export package")]
        static void Export() {
            ScriptableWizard.DisplayWizard("Exporter", typeof(Menu_PackageExporter), "Export");
        }

        private void OnWizardUpdate() {
            var b = new PackageBuilder();
            assets = b.GetAssetPaths();
        }

        private void OnWizardCreate() {
            string targetFilePath = EditorUtility.SaveFilePanel("Save package", "", "minamo", "unitypackage");
            if (targetFilePath == "") {
                return;
            }
            AssetDatabase.ExportPackage(assets, targetFilePath);
        }
    }
}
