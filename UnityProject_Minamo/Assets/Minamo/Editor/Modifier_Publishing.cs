using System.Collections.Generic;
using System.Text;
using UnityEditor;
using System.Linq;

namespace Assets.Minamo.Editor {
    class Modifier_Publishing : IModifier {
        // android
        AssignableType<bool> useApkExpansion;

        string[] uwpCapability;

        // ps4
        AssignableType<bool> ps4_attribExclusiveVR;
        AssignableType<bool> ps4_attribShareSupport;
        AssignableType<bool> ps4_attribMoveSupport;
        AssignableType<PlayerSettings.PS4.PS4AppCategory> ps4_category;
        AssignableType<string> ps4_masterVersion;
        AssignableType<string> ps4_contentID;
        AssignableType<int> ps4_applicationParameter1;
        AssignableType<int> ps4_applicationParameter2;
        AssignableType<int> ps4_applicationParameter3;
        AssignableType<int> ps4_applicationParameter4;
        AssignableType<PlayerSettings.PS4.PS4EnterButtonAssignment> ps4_enterButtonAssignment;


        public void Apply() {
            if (useApkExpansion.Flag) {
                PlayerSettings.Android.useAPKExpansionFiles = useApkExpansion;
            }

            var capabilityList = uwpCapability.ToList();
            var table = StringEnumConverter.Get<PlayerSettings.WSACapability>();
            foreach(var kv in table) {
                var flag = capabilityList.Contains(kv.Key);
                PlayerSettings.WSA.SetCapability(kv.Value, flag);
            }

            if (ps4_attribExclusiveVR.Flag) {
                PlayerSettings.PS4.attribExclusiveVR = ps4_attribExclusiveVR;
            }
            if (ps4_attribShareSupport.Flag) {
                PlayerSettings.PS4.attribShareSupport = ps4_attribShareSupport;
            }
            if (ps4_attribMoveSupport.Flag) {
                PlayerSettings.PS4.attribMoveSupport = ps4_attribMoveSupport;
            }
            if (ps4_category.Flag) {
                PlayerSettings.PS4.category = ps4_category;
            }
            if (ps4_masterVersion.Flag) {
                PlayerSettings.PS4.masterVersion = ps4_masterVersion;
            }
            if (ps4_contentID.Flag) {
                PlayerSettings.PS4.contentID = ps4_contentID;
            }
            if (ps4_applicationParameter1.Flag) {
                PlayerSettings.PS4.applicationParameter1 = ps4_applicationParameter1;
            }
            if (ps4_applicationParameter2.Flag) {
                PlayerSettings.PS4.applicationParameter1 = ps4_applicationParameter2;
            }
            if (ps4_applicationParameter3.Flag) {
                PlayerSettings.PS4.applicationParameter1 = ps4_applicationParameter3;
            }
            if (ps4_applicationParameter4.Flag) {
                PlayerSettings.PS4.applicationParameter1 = ps4_applicationParameter4;
            }
            if (ps4_enterButtonAssignment.Flag) {
                PlayerSettings.PS4.enterButtonAssignment = ps4_enterButtonAssignment;
            }
        }

        public void Reload(AnyDictionary dict) {
            {
                bool outval = false;
                if(dict.TryGetValue<bool>("useApkExpansion", out outval)) {
                    useApkExpansion = AssignableType<bool>.Create(outval);
                }
            }
            

            var l = dict.GetList("uwpCapability");
            var list = new List<string>();
            for(int i = 0; l != null && i < l.Count; i++) {
                var v = l[i] as string;
                if(v != null) {
                    list.Add(v);
                }
            }
            this.uwpCapability = list.ToArray();

            {
                bool outval = false;
                if(dict.TryGetValue<bool>("ps4_attribExclusiveVR", out outval)) {
                    ps4_attribExclusiveVR = AssignableType<bool>.Create(outval);
                }
            }
            {
                bool outval = false;
                if(dict.TryGetValue<bool>("ps4_attribShareSupport", out outval)) {
                    ps4_attribShareSupport = AssignableType<bool>.Create(outval);
                }
            }
            {
                bool outval = false;
                if(dict.TryGetValue("ps4_attribMoveSupport", out outval)) {
                    ps4_attribMoveSupport = AssignableType<bool>.Create(outval);
                }
            }
            {
                var str = dict.GetValue<string>("ps4_category");
                if(str != null) {
                    var d = StringEnumConverter.Get<PlayerSettings.PS4.PS4AppCategory>();
                    ps4_category = AssignableType<PlayerSettings.PS4.PS4AppCategory>.Create(d[str]);
                }
            }
            {
                string outval = "";
                if(dict.TryGetValue("ps4_masterVersion", out outval)) {
                    ps4_masterVersion = AssignableType<string>.Create(outval);
                }
            }
            {
                string outval = "";
                if(dict.TryGetValue("ps4_contentID", out outval)) {
                    ps4_contentID = AssignableType<string>.Create(outval);
                }
            }
            {
                int outval = 0;
                if (dict.TryGetValue("ps4_applicationParameter1", out outval)) {
                    ps4_applicationParameter1 = AssignableType<int>.Create(outval);
                }
            }
            {
                int outval = 0;
                if (dict.TryGetValue("ps4_applicationParameter2", out outval)) {
                    ps4_applicationParameter2 = AssignableType<int>.Create(outval);
                }
            }
            {
                int outval = 0;
                if (dict.TryGetValue("ps4_applicationParameter3", out outval)) {
                    ps4_applicationParameter3 = AssignableType<int>.Create(outval);
                }
            }
            {
                int outval = 0;
                if (dict.TryGetValue("ps4_applicationParameter4", out outval)) {
                    ps4_applicationParameter4 = AssignableType<int>.Create(outval);
                }
            }
            {
                var str = dict.GetValue<string>("ps4_category");
                if (str != null) {
                    var d = StringEnumConverter.Get<PlayerSettings.PS4.PS4EnterButtonAssignment>();
                    ps4_enterButtonAssignment = AssignableType<PlayerSettings.PS4.PS4EnterButtonAssignment>.Create(d[str]);
                }
            }
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
                useApkExpansion = AssignableType<bool>.Create(PlayerSettings.Android.useAPKExpansionFiles),
                uwpCapability = capabilityList.ToArray(),

                ps4_attribExclusiveVR = AssignableType<bool>.Create(PlayerSettings.PS4.attribExclusiveVR),
                ps4_attribShareSupport = AssignableType<bool>.Create(PlayerSettings.PS4.attribShareSupport),
                ps4_attribMoveSupport = AssignableType<bool>.Create(PlayerSettings.PS4.attribMoveSupport),
                ps4_category = AssignableType<PlayerSettings.PS4.PS4AppCategory>.Create(PlayerSettings.PS4.category),
                ps4_masterVersion = AssignableType<string>.Create(PlayerSettings.PS4.masterVersion),
                ps4_contentID = AssignableType<string>.Create(PlayerSettings.PS4.contentID),
                ps4_applicationParameter1 = AssignableType<int>.Create(PlayerSettings.PS4.applicationParameter1),
                ps4_applicationParameter2 = AssignableType<int>.Create(PlayerSettings.PS4.applicationParameter2),
                ps4_applicationParameter3 = AssignableType<int>.Create(PlayerSettings.PS4.applicationParameter3),
                ps4_applicationParameter4 = AssignableType<int>.Create(PlayerSettings.PS4.applicationParameter4),
                ps4_enterButtonAssignment = AssignableType<PlayerSettings.PS4.PS4EnterButtonAssignment>.Create(PlayerSettings.PS4.enterButtonAssignment),
            };
        }

        void AppendConfigKeyValue(StringBuilder sb, string key, object value) {
            sb.AppendFormat("{0}={1}, ", key, value);
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            if (useApkExpansion.Flag) {
                AppendConfigKeyValue(sb, "useApkExpansion", useApkExpansion);
            }

            AppendConfigKeyValue(sb, "uwpCapability", string.Join("|", uwpCapability));

            // ps4 attributes

            if(ps4_attribExclusiveVR.Flag) {
                AppendConfigKeyValue(sb, "ps4_attribExclusiveVR", ps4_attribExclusiveVR);
            };
            if (ps4_attribShareSupport.Flag) {
                AppendConfigKeyValue(sb, "ps4_attribShareSupport", ps4_attribShareSupport);
            }
            if (ps4_attribMoveSupport.Flag) {
                AppendConfigKeyValue(sb, "ps4_attribMoveSupport", ps4_attribMoveSupport);
            }
            if (ps4_category.Flag) {
                AppendConfigKeyValue(sb, "ps4_category", ps4_category);
            }
            if (ps4_masterVersion.Flag) {
                AppendConfigKeyValue(sb, "ps4_masterVersion", ps4_masterVersion);
            }
            if (ps4_contentID.Flag) {
                AppendConfigKeyValue(sb, "ps4_contentID", ps4_contentID);
            }
            if (ps4_applicationParameter1.Flag) {
                AppendConfigKeyValue(sb, "ps4_applicationParameter1", ps4_applicationParameter1);
            }
            if (ps4_applicationParameter2.Flag) {
                AppendConfigKeyValue(sb, "ps4_applicationParameter2", ps4_applicationParameter2);
            }
            if (ps4_applicationParameter3.Flag) {
                AppendConfigKeyValue(sb, "ps4_applicationParameter3", ps4_applicationParameter3);
            }
            if (ps4_applicationParameter4.Flag) {
                AppendConfigKeyValue(sb, "ps4_applicationParameter4", ps4_applicationParameter4);
            }
            if (ps4_enterButtonAssignment.Flag) {
                AppendConfigKeyValue(sb, "ps4_enterButtonAssignment", ps4_enterButtonAssignment);
            }
            return sb.ToString();
        }
    }
}
