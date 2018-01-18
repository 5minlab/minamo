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
                { typeof(PlayerSettings.WSACapability), ForWSACapability() },
                { typeof(WSASubtarget), ForStringEnumDictionary() },
                { typeof(WSAUWPBuildType), ForWSAUWPBuildType() },
                { typeof(WSABuildAndRunDeployTarget), ForWSABuildAndRunDeployTarget() },
                { typeof(PS4BuildSubtarget), ForPS4BuildSubtarget() },
                { typeof(PS4HardwareTarget), ForPS4HardwareTarget() },
                { typeof(PlayerSettings.PS4.PS4AppCategory), ForPS4AppCategory() },
                { typeof(PlayerSettings.PS4.PS4EnterButtonAssignment), ForPS4EnterButtonAssignment() }
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
#if UNITY_2017_3_OR_NEWER
                { "osx", BuildTarget.StandaloneOSX },
#else
                { "osx", BuildTarget.StandaloneOSXUniversal },
#endif
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
                { "buildScriptsOnly",  BuildOptions.BuildScriptsOnly },
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

        static StringEnumDictionary<WSASubtarget> ForStringEnumDictionary() {
            var dict = new Dictionary<string, WSASubtarget>()
            {
                { "AnyDevice", WSASubtarget.AnyDevice },
                { "PC", WSASubtarget.PC },
                { "Mobile", WSASubtarget.Mobile },
                { "HoloLens", WSASubtarget.HoloLens },
            };
            return new StringEnumDictionary<WSASubtarget>(dict, WSASubtarget.AnyDevice);
        }

        static StringEnumDictionary<WSAUWPBuildType> ForWSAUWPBuildType() {
            var dict = new Dictionary<string, WSAUWPBuildType>()
            {
                { "XAML", WSAUWPBuildType.XAML },
                { "D3D", WSAUWPBuildType.D3D },
            };
            return new StringEnumDictionary<WSAUWPBuildType>(dict, WSAUWPBuildType.D3D);
        }

        static StringEnumDictionary<WSABuildAndRunDeployTarget> ForWSABuildAndRunDeployTarget() {
            var dict = new Dictionary<string, WSABuildAndRunDeployTarget>()
            {
                { "LocalMachine", WSABuildAndRunDeployTarget.LocalMachine },
                { "WindowsPhone", WSABuildAndRunDeployTarget.WindowsPhone },
                { "LocalMachineAndWindowsPhone", WSABuildAndRunDeployTarget.LocalMachineAndWindowsPhone },
            };
            return new StringEnumDictionary<WSABuildAndRunDeployTarget>(dict, WSABuildAndRunDeployTarget.LocalMachine);
        }

        static StringEnumDictionary<PlayerSettings.WSACapability> ForWSACapability() {
            var dict = new Dictionary<string, PlayerSettings.WSACapability>()
            {
                { "EnterpriseAuthentication", PlayerSettings.WSACapability.EnterpriseAuthentication },
                { "InternetClient", PlayerSettings.WSACapability.InternetClient},
                { "InternetClientServer", PlayerSettings.WSACapability.InternetClientServer},
                { "MusicLibrary",  PlayerSettings.WSACapability.MusicLibrary },
                { "PicturesLibrary",  PlayerSettings.WSACapability.PicturesLibrary },
                { "PrivateNetworkClientServer", PlayerSettings.WSACapability.PrivateNetworkClientServer },
                { "RemovableStorage", PlayerSettings.WSACapability.RemovableStorage },
                { "SharedUserCertificates", PlayerSettings.WSACapability.SharedUserCertificates },
                { "VideosLibrary", PlayerSettings.WSACapability.VideosLibrary },
                { "WebCam", PlayerSettings.WSACapability.WebCam },
                { "Proximity", PlayerSettings.WSACapability.Proximity },
                { "Microphone", PlayerSettings.WSACapability.Microphone },
                { "Location", PlayerSettings.WSACapability.Location },
                { "HumanInterfaceDevice", PlayerSettings.WSACapability.HumanInterfaceDevice },
                { "AllJoyn", PlayerSettings.WSACapability.AllJoyn },
                { "BlockedChatMessages", PlayerSettings.WSACapability.BlockedChatMessages },
                { "Chat", PlayerSettings.WSACapability.Chat },
                { "CodeGeneration", PlayerSettings.WSACapability.CodeGeneration },
                { "Objects3D", PlayerSettings.WSACapability.Objects3D },
                { "PhoneCall", PlayerSettings.WSACapability.PhoneCall },
                { "UserAccountInformation", PlayerSettings.WSACapability.UserAccountInformation },
                { "VoipCall", PlayerSettings.WSACapability.VoipCall },
                { "Bluetooth", PlayerSettings.WSACapability.Bluetooth },
                { "SpatialPerception", PlayerSettings.WSACapability.SpatialPerception },
                { "InputInjectionBrokered", PlayerSettings.WSACapability.InputInjectionBrokered },
            };
            return new StringEnumDictionary<PlayerSettings.WSACapability>(dict, (PlayerSettings.WSACapability)0);
        }

        static StringEnumDictionary<PS4BuildSubtarget> ForPS4BuildSubtarget() {
            var dict = new Dictionary<string, PS4BuildSubtarget>()
            {
                { "PCHosted", PS4BuildSubtarget.PCHosted },
                { "Package", PS4BuildSubtarget.Package },
                { "ISO", PS4BuildSubtarget.Iso },
            };
            return new StringEnumDictionary<PS4BuildSubtarget>(dict, PS4BuildSubtarget.PCHosted);
        }

        static StringEnumDictionary<PS4HardwareTarget> ForPS4HardwareTarget() {
            var dict = new Dictionary<string, PS4HardwareTarget>()
            {
                { "BaseOnly", PS4HardwareTarget.BaseOnly },
                { "NeoAndBase", PS4HardwareTarget.NeoAndBase },
            };
            return new StringEnumDictionary<PS4HardwareTarget>(dict, PS4HardwareTarget.BaseOnly);
        }

        static StringEnumDictionary<PlayerSettings.PS4.PS4AppCategory> ForPS4AppCategory() {
            var dict = new Dictionary<string, PlayerSettings.PS4.PS4AppCategory>()
            {
                { "Application", PlayerSettings.PS4.PS4AppCategory.Application },
                { "Patch", PlayerSettings.PS4.PS4AppCategory.Patch },
                { "Remaster", PlayerSettings.PS4.PS4AppCategory.Remaster },
            };
            return new StringEnumDictionary<PlayerSettings.PS4.PS4AppCategory>(dict, PlayerSettings.PS4.PS4AppCategory.Application);
        }
        static StringEnumDictionary<PlayerSettings.PS4.PS4EnterButtonAssignment> ForPS4EnterButtonAssignment() {
            var dict = new Dictionary<string, PlayerSettings.PS4.PS4EnterButtonAssignment>()
            {
                { "CircleButton", PlayerSettings.PS4.PS4EnterButtonAssignment.CircleButton },
                { "CrossButton", PlayerSettings.PS4.PS4EnterButtonAssignment.CrossButton },
            };
            return new StringEnumDictionary<PlayerSettings.PS4.PS4EnterButtonAssignment>(dict, PlayerSettings.PS4.PS4EnterButtonAssignment.CircleButton);
        }
    }
}
