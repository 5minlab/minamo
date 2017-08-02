using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class StringEnumConverter {
        static readonly Dictionary<Type, object> table;

        static StringEnumConverter() {
            table = new Dictionary<Type, object>()
            {
                { typeof(BuildTarget), ForBuildTarget() },
                { typeof(BuildTargetGroup), ForBuildTargetGroup() },
                { typeof(BuildOptions), ForBuildOptions() },
                { typeof(StereoRenderingPath), ForStereoRenderingPath() },
            };
        }

        internal static StringEnumDictionary<T> Get<T>() {
            var t = typeof(T);
            var dict = table[t];
            var retval = dict as StringEnumDictionary<T>;
            Debug.AssertFormat(retval != null, "invalid string enum table type : {0}", t);
            return retval;
        }

        static StringEnumDictionary<BuildTargetGroup> ForBuildTargetGroup() {
            var dict = new Dictionary<string, BuildTargetGroup>()
            {
                { "android", BuildTargetGroup.Android },
                { "ios", BuildTargetGroup.iOS },
                { "standalone", BuildTargetGroup.Standalone },
            };
            return new StringEnumDictionary<BuildTargetGroup>(dict, BuildTargetGroup.Standalone);
        }

        static StringEnumDictionary<BuildTarget> ForBuildTarget() {
            var dict = new Dictionary<string, BuildTarget>()
            {
                { "android", BuildTarget.Android },
                { "ios", BuildTarget.iOS },

                { "windows", BuildTarget.StandaloneWindows },
                { "windows64", BuildTarget.StandaloneWindows64 },

                { "osx", BuildTarget.StandaloneOSXUniversal },
                { "linux", BuildTarget.StandaloneLinuxUniversal },
            };
            return new StringEnumDictionary<BuildTarget>(dict, BuildTarget.NoTarget);
        }

        static StringEnumDictionary<StereoRenderingPath> ForStereoRenderingPath() {
            var dict = new Dictionary<string, StereoRenderingPath>()
            {
                { "multi-pass", StereoRenderingPath.MultiPass },
                { "single-pass", StereoRenderingPath.SinglePass },
                { "instancing", StereoRenderingPath.Instancing },
            };
            return new StringEnumDictionary<StereoRenderingPath>(dict, StereoRenderingPath.MultiPass);
        }

        static StringEnumDictionary<BuildOptions> ForBuildOptions() {
            var dict = new Dictionary<string, BuildOptions>()
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
            return new StringEnumDictionary<BuildOptions>(dict, BuildOptions.None);
        }
    }
}
