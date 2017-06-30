using System.Collections.Generic;
using UnityEditor;

namespace Assets.Minamo.Editor {
    class Helper {
        static readonly Dictionary<string, BuildTargetGroup> buildtargetTable = new Dictionary<string, BuildTargetGroup>()
        {
            { "android", BuildTargetGroup.Android },
            { "ios", BuildTargetGroup.iOS },
            { "standalone", BuildTargetGroup.Standalone },
        };

        static readonly Dictionary<string, BuildOptions> buildOptionTable = new Dictionary<string, BuildOptions>()
        {
            { "development", BuildOptions.Development },
            { "allowDebugging", BuildOptions.AllowDebugging },
            { "acceptExternalModificationsToPlayer", BuildOptions.AcceptExternalModificationsToPlayer },
            { "connectWithProfiler", BuildOptions.ConnectWithProfiler },
            { "showBuiltPlayer", BuildOptions.ShowBuiltPlayer },
            { "autoRunPlayer", BuildOptions.AutoRunPlayer },
            { "symlinkLibraries", BuildOptions.SymlinkLibraries },
            { "forceEnableAssertions", BuildOptions.ForceEnableAssertions },
        };

        static readonly Dictionary<string, BuildTarget> buildTargetTable = new Dictionary<string, BuildTarget>()
        {
            { "android", BuildTarget.Android },
            { "ios", BuildTarget.iOS },

            { "windows", BuildTarget.StandaloneWindows },
            { "windows64", BuildTarget.StandaloneWindows64 },

            { "osx", BuildTarget.StandaloneOSXUniversal },
            { "linux", BuildTarget.StandaloneLinuxUniversal },
        };

        public static BuildTargetGroup ToBuildTargetGroup(string s) {
            BuildTargetGroup g;
            if(buildtargetTable.TryGetValue(s, out g)) {
                return g;
            }
            return BuildTargetGroup.Standalone;
        }

        public static BuildTarget ToBuildTarget(string s) {
            BuildTarget found;
            if (buildTargetTable.TryGetValue(s, out found)) {
                return found;
            }
            return BuildTarget.NoTarget;
        }

        public static BuildOptions ToBuildOptions(string s) {
            BuildOptions found;
            if (buildOptionTable.TryGetValue(s, out found)) {
                return found;
            }
            return BuildOptions.None;
        }


        public static List<T> Convert<T>(List<object> l) {
            var retval = new List<T>();
            foreach(var el in l) {
                if(el == null) { continue; }
                if(typeof(T).IsAssignableFrom(el.GetType())) {
                    retval.Add((T)el);
                }
            }
            return retval;
        }
    }
}
