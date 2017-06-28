using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class AndroidSdkVersionModifier : IModifier {
        public const string KeyMin = "min";
        public const string KeyTarget = "target";

        AndroidSdkVersions min;
        AndroidSdkVersions target;

        public void Apply() {
            PlayerSettings.Android.minSdkVersion = min;
            PlayerSettings.Android.targetSdkVersion = target;
        }

        public AndroidSdkVersionModifier() { }
        public AndroidSdkVersionModifier(IDictionary<string, int> map) {
            int minval;
            if(!map.TryGetValue(KeyMin, out minval)) {
                Debug.LogFormat("cannot find : {0}", KeyMin);
            }

            int targetval;
            if(!map.TryGetValue(KeyTarget, out targetval)) {
                Debug.LogFormat("cannot find : {0}", KeyTarget);
            }

            min = ConvertVersion(minval);
            target = ConvertVersion(targetval);
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
