using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class Menu_ConfigFile : BaseScriptableWizard {
        [MenuItem("Window/Minamo/Show config")]
        static void ShowConfig() {
            if (BaseScriptableWizard.PreExecute()) {
                ScriptableWizard.DisplayWizard("Uni config file", typeof(Menu_ConfigFile), "Show", "Load");
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

            foreach (var m in modifiers) {
                m.Apply();
            }
            Debug.LogFormat("Uni config loaded : {0}", filePath);
        }

        void Load(string filepath, out IModifier[] modifiers, out PlayerBuildExecutor executor) {
            var jsontext = File.ReadAllText(filepath);
            var config = new Config(jsontext);

            modifiers = config.CreateConfigModifiers();
            executor = config.PlayerBuild;
        }
    }
}
