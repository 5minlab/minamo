using System.Text;
using UnityEditor;

namespace Assets.Minamo.Editor {
    class Modifier_Publishing : IModifier {
        bool useApkExpansion;

        public void Apply() {
            PlayerSettings.Android.useAPKExpansionFiles = useApkExpansion;
        }

        public void Reload(AnyDictionary dict) {
            useApkExpansion = dict.GetValue<bool>("useApkExpansion");
        }

        internal static Modifier_Publishing Current() {
            return new Modifier_Publishing()
            {
                useApkExpansion = PlayerSettings.Android.useAPKExpansionFiles,
            };
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            sb.AppendFormat("useApkExpansion={0}", useApkExpansion);
            return sb.ToString();
        }
    }
}
