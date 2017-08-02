using System.Collections.Generic;
using System.Text;
using UnityEditor;

namespace Assets.Minamo.Editor {
    class Modifier_Keystore : IModifier {
        string keystoreName;
        string keystorePass;
        string keyaliasName;
        string keyaliasPass;

        internal Modifier_Keystore() { }

        public void Reload(AnyDictionary dict) {
            keystoreName = dict.GetValue<string>("keystoreName");
            keystorePass = dict.GetValue<string>("keystorePass");
            keyaliasName = dict.GetValue<string>("keyaliasName");
            keyaliasPass = dict.GetValue<string>("keyaliasPass");
        }

        Modifier_Keystore(Dictionary<string, object> map) {
            var dict = new AnyDictionary(map);

        }

        internal static Modifier_Keystore Current() {
            return new Modifier_Keystore()
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
