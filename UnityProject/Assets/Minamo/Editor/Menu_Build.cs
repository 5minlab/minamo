using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class Menu_Build : BaseScriptableWizard {
        [MenuItem("Window/Minamo/Build")]
        static void Execute() {
            if (BaseScriptableWizard.PreExecute()) {
                ScriptableWizard.DisplayWizard("Uni build", typeof(Menu_Build), "Build");
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
            if (fp == "") {
                return;
            }

            Build.ExecuteCommon(configFilePath, fp);
        }
    }
}
