using System.Collections.Generic;
using System.Text;
using UnityEditor;
using System.Linq;

namespace Assets.Minamo.Editor {
    class Modifier_Publishing : IModifier {
        // android
        bool useApkExpansion;

        string[] uwpCapability;

        public void Apply() {
            PlayerSettings.Android.useAPKExpansionFiles = useApkExpansion;

            var capabilityList = uwpCapability.ToList();
            var table = StringEnumConverter.Get<PlayerSettings.WSACapability>();
            foreach(var kv in table) {
                var flag = capabilityList.Contains(kv.Key);
                PlayerSettings.WSA.SetCapability(kv.Value, flag);
            }
        }

        public void Reload(AnyDictionary dict) {
            useApkExpansion = dict.GetValue<bool>("useApkExpansion");

            var l = dict.GetList("uwpCapability");
            var list = new List<string>();
            for(int i = 0; l != null && i < l.Count; i++) {
                var v = l[i] as string;
                if(v != null) {
                    list.Add(v);
                }
            }
            this.uwpCapability = list.ToArray();
        }

        internal static Modifier_Publishing Current() {
            var capabilityList = new List<string>();
            var table = StringEnumConverter.Get<PlayerSettings.WSACapability>();
            foreach(var kv in table) {
                var f = PlayerSettings.WSA.GetCapability(kv.Value);
                if(f) {
                    capabilityList.Add(kv.Key);
                }
            }

            return new Modifier_Publishing()
            {
                useApkExpansion = PlayerSettings.Android.useAPKExpansionFiles,
                uwpCapability = capabilityList.ToArray(),
            };
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            sb.AppendFormat("useApkExpansion={0}, ", useApkExpansion);
            sb.AppendFormat("uwpCapability={0}, ", string.Join("|", uwpCapability));
            return sb.ToString();
        }
    }
}
