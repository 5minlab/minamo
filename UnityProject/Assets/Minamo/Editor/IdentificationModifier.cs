using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class IdentificationModifier : IModifier {
        public const string KeyPackageName = "packageName";
        public const string KeyVersionName = "versionName";
        public const string KeyVersionCode = "versionCode";

        // common
        string packageName;
        string versionName;

        // android
        int android_versionCode;

        // ios
        string ios_build;

        public IdentificationModifier() { }
        public IdentificationModifier(IDictionary<string, string> map) {
            if (!map.TryGetValue(KeyPackageName, out packageName)) {
                Debug.LogFormat("cannot find key : {0}", KeyPackageName);
            }
            if (!map.TryGetValue(KeyVersionName, out versionName)) {
                Debug.LogFormat("cannot find key : {0}", KeyVersionName);
            }

            string versionCode;
            if (!map.TryGetValue(KeyVersionCode, out versionCode)) {
                Debug.LogFormat("cannot find key : {0}", KeyVersionCode);
            }

            ios_build = versionCode;
            if (!int.TryParse(versionCode, out android_versionCode)) {
                Debug.LogFormat("cannot parse version code to android version code : {0}", versionCode);
                android_versionCode = 0;
            }
        }

        public static IdentificationModifier Current() {
            return new IdentificationModifier()
            {
                packageName = PlayerSettings.applicationIdentifier,
                versionName = PlayerSettings.bundleVersion,

                android_versionCode = PlayerSettings.Android.bundleVersionCode,
                ios_build = PlayerSettings.iOS.buildNumber,
            };
        }

        public void Apply() {
            PlayerSettings.applicationIdentifier = packageName;
            PlayerSettings.bundleVersion = versionName;

            PlayerSettings.Android.bundleVersionCode = android_versionCode;

            PlayerSettings.iOS.buildNumber = ios_build;
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            sb.AppendFormat("packageName={0}, ", packageName);
            sb.AppendFormat("versionName={0}, ", versionName);
            sb.AppendFormat("versionCode={0}, ", ios_build);
            return sb.ToString();
        }
    }
}
