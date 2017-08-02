using System.Collections.Generic;
using System.Text;
using UnityEditor;

namespace Assets.Minamo.Editor {
    class Modifier_DefineSymbol : IModifier {
        readonly BuildTargetGroup targetGroup;
        string defines;

        internal Modifier_DefineSymbol(BuildTargetGroup targetGroup) {
            this.targetGroup = targetGroup;
        }

        public void Reload(AnyDictionary dict) {
            var prev = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);

            var tokens = new List<string>();
            if (prev.Length > 0) {
                tokens.Add(prev);
            }

            for(int i = 0; i < dict.Count; i++) {
                var s = dict.GetAt<string>(i);
                if(s == null || s == "") {
                    continue;
                }
                tokens.Add(s);
            }

            this.defines = string.Join(";", tokens.ToArray());
        }

        internal static Modifier_DefineSymbol Current(BuildTargetGroup g) {
            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(g);
            return new Modifier_DefineSymbol(g)
            {
                defines = defines,
            };
        }

        public void Apply() {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, defines);
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            sb.AppendFormat("target={0}, ", targetGroup);
            sb.AppendFormat("defines={0}", defines);
            return sb.ToString();
        }
    }
}
