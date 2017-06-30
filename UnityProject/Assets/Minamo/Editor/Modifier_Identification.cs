using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class Modifier_Identification : IModifier {
        readonly BuildTargetGroup targetGroup;

        // common
        string packageName;
        string versionName;

        // android
        int android_versionCode;

        // ios
        string ios_build;

        public Modifier_Identification(BuildTargetGroup targetGroup) {
            this.targetGroup = targetGroup;
        }

        public void Reload(AnyDictionary dict) {
            packageName = dict.GetValue<string>("packageName");
            versionName = dict.GetValue<string>("versionName");
            var versionCode = dict.GetValue<string>("versionCode");

            ios_build = versionCode;
            if (!int.TryParse(versionCode, out android_versionCode)) {
                Debug.LogFormat("cannot parse version code to android version code : {0}", versionCode);
                android_versionCode = 0;
            }
        }

        public static Modifier_Identification Current(BuildTargetGroup targetGroup) {
            return new Modifier_Identification(targetGroup)
            {
                packageName = PlayerSettings.GetApplicationIdentifier(targetGroup),
                versionName = PlayerSettings.bundleVersion,

                android_versionCode = PlayerSettings.Android.bundleVersionCode,
                ios_build = PlayerSettings.iOS.buildNumber,
            };
        }

        public void Apply() {
            PlayerSettings.SetApplicationIdentifier(targetGroup, packageName);
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
