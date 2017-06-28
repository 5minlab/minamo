using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class KeystoreModifier : IModifier {
        string keystoreName;
        string keystorePass;
        string keyaliasName;
        string keyaliasPass;

        public KeystoreModifier() { }
        public KeystoreModifier(Dictionary<string, string> map) {
            if(!map.TryGetValue("keystoreName", out keystoreName)) {
                Debug.Log("cannot find keystore name");
            }
            if(!map.TryGetValue("keystorePass", out keystorePass)) {
                Debug.Log("cannot find keystore pass");
            }
            if(!map.TryGetValue("keyaliasName", out keyaliasName)) {
                Debug.Log("cannot find keyalias name");
            }
            if(!map.TryGetValue("keyaliasPass", out keyaliasPass)) {
                Debug.Log("cannot find keyalias pass");
            }
        }

        public static KeystoreModifier Current() {
            return new KeystoreModifier()
            {
                keystoreName = PlayerSettings.Android.keystoreName,
                keystorePass = PlayerSettings.Android.keystorePass,
                keyaliasName = PlayerSettings.Android.keyaliasName,
                keyaliasPass = PlayerSettings.Android.keyaliasPass,
            };
        }

        public void Apply() {
            PlayerSettings.Android.keystoreName = keystoreName;
            PlayerSettings.Android.keystorePass = keystorePass;

            PlayerSettings.Android.keyaliasName = keyaliasName;
            PlayerSettings.Android.keyaliasPass = keyaliasPass;
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            sb.AppendFormat("keystoreName={0}, ", keystoreName);
            sb.AppendFormat("keystorePass={0}, ", keystorePass);
            sb.AppendFormat("keyaliasName={0}, ", keyaliasName);
            sb.AppendFormat("keyaliasPass={0}, ", keyaliasPass);
            return sb.ToString();
        }
    }
}
