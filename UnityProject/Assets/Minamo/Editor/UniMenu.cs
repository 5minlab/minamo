using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class BaseScriptableWizard : ScriptableWizard {
        protected static string lastFilePath = "";

        /// <summary>
        /// open config file
        /// </summary>
        protected static bool PreExecute() {
            string lastDir = "";
            if (lastFilePath != "") {
                lastDir = Path.GetDirectoryName(lastFilePath);
            }

            var fp = EditorUtility.OpenFilePanel("select config file", lastDir, "json");
            if (fp == "") {
                return false;
            }

            lastFilePath = fp;
            return true;
        }
    }

    class ConfigFileMenu : BaseScriptableWizard {
        [MenuItem("Window/Uni/Show config")]
        static void ShowConfig() {
            if (BaseScriptableWizard.PreExecute()) {
                ScriptableWizard.DisplayWizard("Uni config file", typeof(ConfigFileMenu), "Show", "Load");
            }
        }

        [SerializeField]
        string filePath = "";
        private void OnWizardUpdate() {
            filePath = BaseScriptableWizard.lastFilePath;
        }

        private void OnWizardCreate() {
            IModifier[] modifiers;
            PlayerBuildExecutor executor;
            Load(filePath, out modifiers, out executor);

            foreach (var m in modifiers) {
                var name = m.GetType().ToString();
                var tokens = name.Split('.');
                var modifierName = tokens[tokens.Length - 1];
                Debug.LogFormat("{0} : {1}", modifierName, m.GetConfigText());
            }
            Debug.LogFormat("Build : {0}", executor.GetConfigText());
        }

        private void OnWizardOtherButton() {
            IModifier[] modifiers;
            PlayerBuildExecutor executor;
            Load(filePath, out modifiers, out executor);

            foreach(var m in modifiers) {
                m.Apply();
            }
            Debug.LogFormat("Uni config loaded : {0}", filePath);
        }

        void Load(string filepath, out IModifier[] modifiers, out PlayerBuildExecutor executor) {
            var jsontext = File.ReadAllText(filepath);
            var config = new Config(jsontext);

            IModifier[] currModifiers;
            Build.CreateModifiers(config, out currModifiers, out modifiers);
            executor = new PlayerBuildExecutor(config.Build);
        }
    }

    class BuildMenu : BaseScriptableWizard {
        [MenuItem("Window/Uni/Build")]
        static void Execute() {
            if (BaseScriptableWizard.PreExecute()) {
                ScriptableWizard.DisplayWizard("Uni build", typeof(BuildMenu), "Build");
            }
        }

        [SerializeField]
        string configFilePath = "";
        private void OnWizardUpdate() {
            configFilePath = BaseScriptableWizard.lastFilePath;
        }

        static string lastOutputFilePath = "";
        private void OnWizardCreate() {
            // 빌드 저장할 위치 선택
            string lastDir = "";
            if (lastOutputFilePath != "") {
                lastDir = Path.GetDirectoryName(lastOutputFilePath);
            }

            var fp = EditorUtility.SaveFilePanel("select output filepath", lastDir, "build.exe", "");
            if(fp == "") {
                return;
            }

            Build.ExecuteCommon(configFilePath, fp);
        }
    }
}
