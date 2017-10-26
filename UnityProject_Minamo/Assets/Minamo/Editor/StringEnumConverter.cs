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
                { typeof(ApiCompatibilityLevel), ForApiCompatibilityLevel() },
                { typeof(ScriptingImplementation), ForScriptingImplementation() },
                { typeof(ScriptingRuntimeVersion), ForScriptingRuntimeVersion() },
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
                { "tvos", BuildTargetGroup.tvOS },
                { "tizen", BuildTargetGroup.Tizen },
                { "xboxone", BuildTargetGroup.XboxOne },
                { "ps4", BuildTargetGroup.PS4 },
                { "webgl", BuildTargetGroup.WebGL },
                { "wsa", BuildTargetGroup.WSA },
            };
            return new StringEnumDictionary<BuildTargetGroup>(dict, BuildTargetGroup.Standalone);
        }

        static StringEnumDictionary<BuildTarget> ForBuildTarget() {
            // order = build target group
            var dict = new Dictionary<string, BuildTarget>()
            {
                // mobile
                { "android", BuildTarget.Android },
                { "ios", BuildTarget.iOS },

                // standalone
                { "windows", BuildTarget.StandaloneWindows },
                { "windows64", BuildTarget.StandaloneWindows64 },
                { "osx", BuildTarget.StandaloneOSXUniversal },
                { "linux", BuildTarget.StandaloneLinuxUniversal },

                { "tvos", BuildTarget.tvOS },
                { "tizen", BuildTarget.Tizen },

                // console
                { "xboxone", BuildTarget.XboxOne },
                { "ps4", BuildTarget.PS4 },

                { "webgl", BuildTarget.WebGL },
                { "wsa", BuildTarget.WSAPlayer },
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

        static StringEnumDictionary<ApiCompatibilityLevel> ForApiCompatibilityLevel() {
            var dict = new Dictionary<string, ApiCompatibilityLevel>()
            {
                { "NET_2_0", ApiCompatibilityLevel.NET_2_0 },
                { "NET_2_0_Subset", ApiCompatibilityLevel.NET_2_0_Subset },
                { "NET_4_6", ApiCompatibilityLevel.NET_4_6 },
                { "NET_Web", ApiCompatibilityLevel.NET_Web },
                { "NET_Micro", ApiCompatibilityLevel.NET_Micro },
            };
            return new StringEnumDictionary<ApiCompatibilityLevel>(dict, ApiCompatibilityLevel.NET_2_0);
        }

        static StringEnumDictionary<ScriptingImplementation> ForScriptingImplementation() {
            var dict = new Dictionary<string, ScriptingImplementation>()
            {
                { "Mono2x", ScriptingImplementation.Mono2x },
                { "IL2CPP", ScriptingImplementation.IL2CPP },
                { "WinRTDotNET", ScriptingImplementation.WinRTDotNET },
            };
            return new StringEnumDictionary<ScriptingImplementation>(dict, ScriptingImplementation.Mono2x);
        }

        static StringEnumDictionary<ScriptingRuntimeVersion> ForScriptingRuntimeVersion() {
            var dict = new Dictionary<string, ScriptingRuntimeVersion>()
            {
                { "latest", ScriptingRuntimeVersion.Latest },
                { "legacy", ScriptingRuntimeVersion.Legacy },
            };
            return new StringEnumDictionary<ScriptingRuntimeVersion>(dict, ScriptingRuntimeVersion.Legacy);
        }
    }
}
