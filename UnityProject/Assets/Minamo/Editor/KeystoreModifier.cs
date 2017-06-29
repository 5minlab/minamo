using System.Collections.Generic;
using System.Text;
using UnityEditor;

namespace Assets.Minamo.Editor {
    class KeystoreModifier : IModifier {
        string keystoreName;
        string keystorePass;
        string keyaliasName;
        string keyaliasPass;

        public KeystoreModifier() { }

        public void Reload(AnyDictionary dict) {
            keystoreName = dict.GetValue<string>("keystoreName");
            keystorePass = dict.GetValue<string>("keystorePass");
            keyaliasName = dict.GetValue<string>("keyaliasName");
            keyaliasPass = dict.GetValue<string>("keyaliasPass");
        }

        public KeystoreModifier(Dictionary<string, object> map) {
            var dict = new AnyDictionary(map);

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
