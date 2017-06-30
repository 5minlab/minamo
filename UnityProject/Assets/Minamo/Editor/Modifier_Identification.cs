using System.Text;
using UnityEditor;

namespace Assets.Minamo.Editor {
    class Modifier_Identification : IModifier {
        readonly BuildTargetGroup targetGroup;

        // common
        string packageName;
        string versionName;
        // android = version code
        // ios : build number
        int versionCode;

        internal Modifier_Identification(BuildTargetGroup targetGroup) {
            this.targetGroup = targetGroup;
        }

        public void Reload(AnyDictionary dict) {
            packageName = dict.GetValue<string>("packageName");
            versionName = dict.GetValue<string>("versionName");
            versionCode = dict.GetValue<int>("versionCode");
        }

        internal static Modifier_Identification Current(BuildTargetGroup targetGroup) {
            int versionCode = 0;
            if(targetGroup == BuildTargetGroup.Android) {
                versionCode = PlayerSettings.Android.bundleVersionCode;

            } else if(targetGroup == BuildTargetGroup.iOS) {
                if(!int.TryParse(PlayerSettings.iOS.buildNumber, out versionCode)) {
                    versionCode = 0;
                }
            }

            return new Modifier_Identification(targetGroup)
            {
                packageName = PlayerSettings.GetApplicationIdentifier(targetGroup),
                versionName = PlayerSettings.bundleVersion,
                versionCode = versionCode,
            };
        }

        public void Apply() {
            PlayerSettings.SetApplicationIdentifier(targetGroup, packageName);
            PlayerSettings.bundleVersion = versionName;

            if(targetGroup == BuildTargetGroup.Android) {
                PlayerSettings.Android.bundleVersionCode = versionCode;
            } else if(targetGroup == BuildTargetGroup.iOS) {
                PlayerSettings.iOS.buildNumber = versionCode.ToString();
            }
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            sb.AppendFormat("packageName={0}, ", packageName);
            sb.AppendFormat("versionName={0}, ", versionName);
            sb.AppendFormat("versionCode={0}, ", versionCode);
            return sb.ToString();
        }


    }
}
