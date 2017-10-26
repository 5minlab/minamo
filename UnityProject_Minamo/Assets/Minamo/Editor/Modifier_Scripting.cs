using System.Text;
using UnityEditor;

namespace Assets.Minamo.Editor {
    /// <summary>
    /// 분류를 따로 만들기 애매한거 몰아넣기
    /// </summary>
    class Modifier_Scripting : IModifier {
        readonly BuildTargetGroup buildTargetGroup;

        bool flag_backend = false;
        ScriptingImplementation backend;


        bool flag_apiCompatibilityLevel = false;
        ApiCompatibilityLevel apiCompatibilityLevel;

        bool flag_scriptingRuntimeVersion = false;
        ScriptingRuntimeVersion scriptingRuntimeVersion;

        internal Modifier_Scripting(BuildTargetGroup targetGroup) {
            this.buildTargetGroup = targetGroup;
        }

        public void Apply() {
            if(flag_backend) {
                PlayerSettings.SetScriptingBackend(buildTargetGroup, backend);
            }
            if(flag_apiCompatibilityLevel) {
                PlayerSettings.SetApiCompatibilityLevel(buildTargetGroup, apiCompatibilityLevel);
            }
            if(flag_scriptingRuntimeVersion) {
                PlayerSettings.scriptingRuntimeVersion = scriptingRuntimeVersion;
            }
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            if(flag_backend) {
                sb.AppendFormat("Scripting Backend: {0}, ", backend);
            }
            if(flag_apiCompatibilityLevel) {
                sb.AppendFormat("Api Compatible Level: {0}, ", apiCompatibilityLevel);
            }
            if(flag_scriptingRuntimeVersion) {
                sb.AppendFormat("Scripting Runtime Version: {0}, ", scriptingRuntimeVersion);
            }
            return sb.ToString();
        }

        public void Reload(AnyDictionary dict) {
            var apiStr = dict.GetValue<string>("apiCompatibilityLevel");
            if(apiStr != null) {
                var apiDict = StringEnumConverter.Get<ApiCompatibilityLevel>();
                flag_apiCompatibilityLevel = apiDict.MustGetValue(apiStr, out apiCompatibilityLevel);
            } else {
                flag_scriptingRuntimeVersion = false;
            }
            

            var backendStr = dict.GetValue<string>("backend");
            if (backendStr != null) {
                var backendDict = StringEnumConverter.Get<ScriptingImplementation>();
                flag_backend = backendDict.MustGetValue(backendStr, out backend);
            } else {
                flag_backend = false;
            }

            var versionStr = dict.GetValue<string>("scriptingRuntimeVersion");
            if (versionStr != null) {
                var versionDict = StringEnumConverter.Get<ScriptingRuntimeVersion>();
                flag_scriptingRuntimeVersion = versionDict.MustGetValue(versionStr, out scriptingRuntimeVersion);
            } else {
                flag_scriptingRuntimeVersion = false;
            }
        }

        internal static Modifier_Scripting Current(BuildTargetGroup g) {
            var backend = PlayerSettings.GetScriptingBackend(g);
            var api = PlayerSettings.GetApiCompatibilityLevel(g);
            var version = PlayerSettings.scriptingRuntimeVersion;
            return new Modifier_Scripting(g)
            {
                flag_backend = true,
                backend = backend,

                flag_apiCompatibilityLevel = true,
                apiCompatibilityLevel = api,

                flag_scriptingRuntimeVersion = true,
                scriptingRuntimeVersion = version,
            };
        }
    }
}
