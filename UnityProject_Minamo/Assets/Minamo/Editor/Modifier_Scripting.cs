using System.Text;
using UnityEditor;

namespace Assets.Minamo.Editor {
    /// <summary>
    /// 분류를 따로 만들기 애매한거 몰아넣기
    /// </summary>
    class Modifier_Scripting : IModifier {
        readonly BuildTargetGroup buildTargetGroup;

        AssignableType<ScriptingImplementation> backend;
        AssignableType<ApiCompatibilityLevel> apiCompatibilityLevel;
        AssignableType<ScriptingRuntimeVersion> scriptingRuntimeVersion;

        internal Modifier_Scripting(BuildTargetGroup targetGroup) {
            this.buildTargetGroup = targetGroup;
        }

        public void Apply() {
            if(backend.Flag) {
                PlayerSettings.SetScriptingBackend(buildTargetGroup, backend);
            }
            if(apiCompatibilityLevel.Flag) {
                PlayerSettings.SetApiCompatibilityLevel(buildTargetGroup, apiCompatibilityLevel);
            }
            if(scriptingRuntimeVersion.Flag) {
                PlayerSettings.scriptingRuntimeVersion = scriptingRuntimeVersion;
            }
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            if(backend.Flag) {
                sb.AppendFormat("Scripting Backend: {0}, ", backend);
            }
            if(apiCompatibilityLevel.Flag) {
                sb.AppendFormat("Api Compatible Level: {0}, ", apiCompatibilityLevel);
            }
            if(scriptingRuntimeVersion.Flag) {
                sb.AppendFormat("Scripting Runtime Version: {0}, ", scriptingRuntimeVersion);
            }
            return sb.ToString();
        }

        public void Reload(AnyDictionary dict) {
            var apiStr = dict.GetValue<string>("apiCompatibilityLevel");
            if(apiStr != null) {
                var apiDict = StringEnumConverter.Get<ApiCompatibilityLevel>();
                apiCompatibilityLevel = AssignableType<ApiCompatibilityLevel>.Create(apiDict[apiStr]);
            }
            
            var backendStr = dict.GetValue<string>("backend");
            if (backendStr != null) {
                var backendDict = StringEnumConverter.Get<ScriptingImplementation>();
                backend = AssignableType<ScriptingImplementation>.Create(backendDict[backendStr]);
            }

            var versionStr = dict.GetValue<string>("scriptingRuntimeVersion");
            if (versionStr != null) {
                var versionDict = StringEnumConverter.Get<ScriptingRuntimeVersion>();
                scriptingRuntimeVersion = AssignableType<ScriptingRuntimeVersion>.Create(versionDict[versionStr]);
            }
        }

        internal static Modifier_Scripting Current(BuildTargetGroup g) {
            var backend = PlayerSettings.GetScriptingBackend(g);
            var api = PlayerSettings.GetApiCompatibilityLevel(g);
            var version = PlayerSettings.scriptingRuntimeVersion;
            return new Modifier_Scripting(g)
            {
                backend = AssignableType<ScriptingImplementation>.Create(backend),
                apiCompatibilityLevel = AssignableType<ApiCompatibilityLevel>.Create(api),
                scriptingRuntimeVersion = AssignableType<ScriptingRuntimeVersion>.Create(version),
            };
        }
    }
}
