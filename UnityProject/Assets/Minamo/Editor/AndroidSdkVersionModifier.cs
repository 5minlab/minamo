using System.Text;
using UnityEditor;

namespace Assets.Minamo.Editor {
    class AndroidSdkVersionModifier : IModifier {
        AndroidSdkVersions min;
        AndroidSdkVersions target;

        public void Apply() {
            PlayerSettings.Android.minSdkVersion = min;
            PlayerSettings.Android.targetSdkVersion = target;
        }

        public void Reload(AnyDictionary dict) {
            min = ConvertVersion(dict.GetValue<int>("min"));
            target = ConvertVersion(dict.GetValue<int>("target"));
        }

        public static AndroidSdkVersionModifier Current() {
            return new AndroidSdkVersionModifier()
            {
                min = PlayerSettings.Android.minSdkVersion,
                target = PlayerSettings.Android.targetSdkVersion,
            };
        }

        static AndroidSdkVersions ConvertVersion(int v) {
            var ver = (AndroidSdkVersions)v;
            return ver;
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            sb.AppendFormat("min={0}, ", min);
            sb.AppendFormat("target={0}", target);
            return sb.ToString();
        }
    }
}
