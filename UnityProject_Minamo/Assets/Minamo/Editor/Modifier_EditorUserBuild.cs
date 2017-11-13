using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.Minamo.Editor {
    class Modifier_EditorUserBuild : IModifier {
        AssignableType<bool> wsaGenerateReferenceProjects;

        public void Apply() {
            if(wsaGenerateReferenceProjects.Flag) {
                EditorUserBuildSettings.wsaGenerateReferenceProjects = wsaGenerateReferenceProjects;
            }
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            if(wsaGenerateReferenceProjects.Flag) {
                sb.AppendFormat("wsaGenerateReferenceProjects: {0}", wsaGenerateReferenceProjects);
            }
            return sb.ToString();
        }

        public void Reload(AnyDictionary dict) {
            wsaGenerateReferenceProjects = AssignableType<bool>.FromDict(dict, "wsaGenerateReferenceProjects");
        }

        internal static Modifier_EditorUserBuild Current() {
            var wsaGenerateReferenceProjects = EditorUserBuildSettings.wsaGenerateReferenceProjects;

            return new Modifier_EditorUserBuild()
            {
                wsaGenerateReferenceProjects = AssignableType<bool>.Create(wsaGenerateReferenceProjects),
            };
        }
    }
}
