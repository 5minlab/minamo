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
            var cb = new ConfigTextBuilder();
            cb.Append("Scripting Backend", backend);
            cb.Append("Api Compatible Level", apiCompatibilityLevel);
            cb.Append("Scripting Runtime Version", scriptingRuntimeVersion);
            return cb.ToString();
        }

        public void Reload(AnyDictionary dict) {
            apiCompatibilityLevel = AssignableType<ApiCompatibilityLevel>.FromEnumDict(dict, "apiCompatibilityLevel");
            backend = AssignableType<ScriptingImplementation>.FromEnumDict(dict, "backend");
            scriptingRuntimeVersion = AssignableType<ScriptingRuntimeVersion>.FromEnumDict(dict, "scriptingRuntimeVersion");
        }

        internal static Modifier_Scripting Current(BuildTargetGroup g) {
            return new Modifier_Scripting(g)
            {
                backend = AssignableType<ScriptingImplementation>.Create(PlayerSettings.GetScriptingBackend(g)),
                apiCompatibilityLevel = AssignableType<ApiCompatibilityLevel>.Create(PlayerSettings.GetApiCompatibilityLevel(g)),
                scriptingRuntimeVersion = AssignableType<ScriptingRuntimeVersion>.Create(PlayerSettings.scriptingRuntimeVersion),
            };
        }
    }
}
