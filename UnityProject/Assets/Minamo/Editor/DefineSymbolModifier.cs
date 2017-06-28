using System.Collections.Generic;
using System.Text;
using UnityEditor;

namespace Assets.Minamo.Editor {
    class DefineSymbolModifier : IModifier {
        string defines;
        BuildTargetGroup targetGroup;

        public DefineSymbolModifier() { }
        public DefineSymbolModifier(BuildTargetGroup targetGroup, string[] arr) {
            this.targetGroup = targetGroup;

            var prev = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
            var tokens = new List<string>();
            if (prev.Length > 0) {
                tokens.Add(prev);
            }
            tokens.AddRange(arr);
            this.defines = string.Join(";", tokens.ToArray());
        }

        public static DefineSymbolModifier Current(BuildTargetGroup g) {
            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(g);
            return new DefineSymbolModifier()
            {
                defines = defines,
                targetGroup = g,
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
