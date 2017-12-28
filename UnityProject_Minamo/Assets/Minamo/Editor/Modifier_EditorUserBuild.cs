using System.Text;
using UnityEditor;

namespace Assets.Minamo.Editor {
    class Modifier_EditorUserBuild : IModifier {
        AssignableType<WSASubtarget> wsaSubtarget;
        AssignableType<WSAUWPBuildType> wsaUWPBuildType;
        /// <summary>
        /// "" : latest installed
        /// "10.0.16299.0" : version based
        /// </summary>
        AssignableType<string> wsaUWPSDK;
        AssignableType<WSABuildAndRunDeployTarget> wsaBuildAndRunDeployTarget;
        AssignableType<bool> wsaGenerateReferenceProjects;

        AssignableType<PS4BuildSubtarget> ps4BuildSubtarget;
        AssignableType<PS4HardwareTarget> ps4HardwareTarget;

        AssignableType<bool> compressWithPsArc;
        AssignableType<bool> compressFilesInPackage;
        AssignableType<bool> explicitNullChecks;
        AssignableType<bool> explicitDivideByZeroChecks;

        public void Apply() {
            if(wsaSubtarget.Flag) {
                EditorUserBuildSettings.wsaSubtarget = wsaSubtarget;
            }
            if(wsaUWPBuildType.Flag) {
                EditorUserBuildSettings.wsaUWPBuildType = wsaUWPBuildType;
            }
            if(wsaUWPSDK.Flag) {
                EditorUserBuildSettings.wsaUWPSDK = wsaUWPSDK;
            }
            if(wsaBuildAndRunDeployTarget.Flag) {
                EditorUserBuildSettings.wsaBuildAndRunDeployTarget = wsaBuildAndRunDeployTarget;
            }
            if(wsaGenerateReferenceProjects.Flag) {
                EditorUserBuildSettings.wsaGenerateReferenceProjects = wsaGenerateReferenceProjects;
            }

            if(ps4BuildSubtarget.Flag) {
                EditorUserBuildSettings.ps4BuildSubtarget = ps4BuildSubtarget;
            }
            if(ps4HardwareTarget.Flag) {
                EditorUserBuildSettings.ps4HardwareTarget = ps4HardwareTarget;
            }

            if (compressWithPsArc.Flag) {
                EditorUserBuildSettings.compressWithPsArc = compressWithPsArc;
            }
            if(compressFilesInPackage.Flag) {
                EditorUserBuildSettings.compressFilesInPackage = compressFilesInPackage;
            }
            if (explicitNullChecks.Flag) {
                EditorUserBuildSettings.explicitNullChecks = explicitNullChecks;
            }
            if (explicitDivideByZeroChecks.Flag) {
                EditorUserBuildSettings.explicitDivideByZeroChecks = explicitDivideByZeroChecks;
            }
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            if(wsaSubtarget.Flag) {
                sb.AppendFormat("wsaSubtarget: {0}, ", wsaSubtarget);
            }
            if(wsaUWPBuildType.Flag) {
                sb.AppendFormat("wsaUWPBuildType: {0}, ", wsaUWPBuildType);
            }
            if(wsaUWPSDK.Flag) {
                sb.AppendFormat("wsaUWPSDK: {0}, ", wsaUWPSDK);
            }
            if(wsaBuildAndRunDeployTarget.Flag) {
                sb.AppendFormat("wsaBuildAndRunDeployTarget: {0}, ", wsaBuildAndRunDeployTarget);
            }
            if(wsaGenerateReferenceProjects.Flag) {
                sb.AppendFormat("wsaGenerateReferenceProjects: {0}, ", wsaGenerateReferenceProjects);
            }

            if(ps4BuildSubtarget.Flag) {
                sb.AppendFormat("ps4BuildSubtarget: {0}", ps4BuildSubtarget);
            }
            if(ps4HardwareTarget.Flag) {
                sb.AppendFormat("ps4HardwareTarget: {0}", ps4HardwareTarget);
            }

            if(compressWithPsArc.Flag) {
                sb.AppendFormat("compressWithPsArc: {0}", compressWithPsArc);
            }
            if(compressFilesInPackage.Flag) {
                sb.AppendFormat("compressFilesInPackage: {0}", compressFilesInPackage);
            }
            if(explicitNullChecks.Flag) {
                sb.AppendFormat("explicitNullChecks: {0}", explicitNullChecks);
            }
            if(explicitDivideByZeroChecks.Flag) {
                sb.AppendFormat("explicitDivideByZeroChecks: {0}", explicitDivideByZeroChecks);
            }
            return sb.ToString();
        }

        public void Reload(AnyDictionary dict) {
            var wsaSubtargetStr = dict.GetValue<string>("wsaSubtarget");
            if(wsaSubtargetStr != null) {
                var d = StringEnumConverter.Get<WSASubtarget>();
                wsaSubtarget = AssignableType<WSASubtarget>.Create(d[wsaSubtargetStr]);
            }

            var wsaUWPBuildTypeStr = dict.GetValue<string>("wsaUWPBuildType");
            if (wsaUWPBuildTypeStr != null) {
                var d = StringEnumConverter.Get<WSAUWPBuildType>();
                wsaUWPBuildType = AssignableType<WSAUWPBuildType>.Create(d[wsaUWPBuildTypeStr]);
            }

            var wsaUWPSDKStr = dict.GetValue<string>("wsaUWPSDK");
            if (wsaUWPSDKStr != null) {
                wsaUWPSDK = AssignableType<string>.Create(wsaUWPSDKStr);
            }

            var wsaBuildAndRunDeployTargetStr = dict.GetValue<string>("wsaBuildAndRunDeployTarget");
            if(wsaBuildAndRunDeployTargetStr != null) {
                var d = StringEnumConverter.Get<WSABuildAndRunDeployTarget>();
                wsaBuildAndRunDeployTarget = AssignableType<WSABuildAndRunDeployTarget>.Create(d[wsaBuildAndRunDeployTargetStr]);
            }

            wsaGenerateReferenceProjects = AssignableType<bool>.FromDict(dict, "wsaGenerateReferenceProjects");

            var ps4BuildSubtargetStr = dict.GetValue<string>("ps4BuildSubtarget");
            if(ps4BuildSubtargetStr != null) {
                var d = StringEnumConverter.Get<PS4BuildSubtarget>();
                ps4BuildSubtarget = AssignableType<PS4BuildSubtarget>.Create(d[ps4BuildSubtargetStr]);
            }

            var ps4HardwareTargetStr = dict.GetValue<string>("ps4HardwareTarget");
            if (ps4HardwareTargetStr != null) {
                var d = StringEnumConverter.Get<PS4HardwareTarget>();
                ps4HardwareTarget = AssignableType<PS4HardwareTarget>.Create(d[ps4HardwareTargetStr]);
            }

            compressWithPsArc = AssignableType<bool>.FromDict(dict, "compressWithPsArc");
            compressFilesInPackage = AssignableType<bool>.FromDict(dict, "compressFilesInPackage");
            explicitNullChecks = AssignableType<bool>.FromDict(dict, "explicitNullChecks");
            explicitDivideByZeroChecks = AssignableType<bool>.FromDict(dict, "explicitDivideByZeroChecks");
        }

        internal static Modifier_EditorUserBuild Current() {
            var wsaSubtarget = EditorUserBuildSettings.wsaSubtarget;
            var wsaUWPBuildType = EditorUserBuildSettings.wsaUWPBuildType;
            var wsaUWPSDK = EditorUserBuildSettings.wsaUWPSDK;
            var wsaBuildAndRunDeployTarget = EditorUserBuildSettings.wsaBuildAndRunDeployTarget;
            var wsaGenerateReferenceProjects = EditorUserBuildSettings.wsaGenerateReferenceProjects;

            return new Modifier_EditorUserBuild()
            {
                wsaSubtarget = AssignableType<WSASubtarget>.Create(wsaSubtarget),
                wsaUWPBuildType = AssignableType<WSAUWPBuildType>.Create(wsaUWPBuildType),
                wsaUWPSDK = AssignableType<string>.Create(wsaUWPSDK),
                wsaBuildAndRunDeployTarget = AssignableType<WSABuildAndRunDeployTarget>.Create(wsaBuildAndRunDeployTarget),
                wsaGenerateReferenceProjects = AssignableType<bool>.Create(wsaGenerateReferenceProjects),

                ps4BuildSubtarget = AssignableType<PS4BuildSubtarget>.Create(EditorUserBuildSettings.ps4BuildSubtarget),
                ps4HardwareTarget = AssignableType<PS4HardwareTarget>.Create(EditorUserBuildSettings.ps4HardwareTarget),

                compressWithPsArc = AssignableType<bool>.Create(EditorUserBuildSettings.compressWithPsArc),
                compressFilesInPackage = AssignableType<bool>.Create(EditorUserBuildSettings.compressFilesInPackage),
                explicitNullChecks = AssignableType<bool>.Create(EditorUserBuildSettings.explicitNullChecks),
                explicitDivideByZeroChecks = AssignableType<bool>.Create(EditorUserBuildSettings.explicitDivideByZeroChecks),
            };
        }
    }
}
