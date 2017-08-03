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
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            if(flag_backend) {
                sb.AppendFormat("Scripting Backend: {0},", backend);
            }
            if(flag_apiCompatibilityLevel) {
                sb.AppendFormat("Api Compatible Level: {0},", apiCompatibilityLevel);
            }
            return sb.ToString();
        }

        public void Reload(AnyDictionary dict) {
            var apiStr = dict.GetValue<string>("apiCompatibilityLevel");
            var apiDict = StringEnumConverter.Get<ApiCompatibilityLevel>();
            flag_apiCompatibilityLevel = apiDict.MustGetValue(apiStr, out apiCompatibilityLevel);

            var backendStr = dict.GetValue<string>("backend");
            var backendDict = StringEnumConverter.Get<ScriptingImplementation>();
            flag_backend = backendDict.MustGetValue(backendStr, out backend);
        }

        internal static Modifier_Scripting Current(BuildTargetGroup g) {
            var backend = PlayerSettings.GetScriptingBackend(g);
            var api = PlayerSettings.GetApiCompatibilityLevel(g);
            return new Modifier_Scripting(g)
            {
                flag_backend = true,
                backend = backend,

                flag_apiCompatibilityLevel = true,
                apiCompatibilityLevel = api,
            };
        }
    }
}
