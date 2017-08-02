using System.IO;
using UnityEditor;

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
}
