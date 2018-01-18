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

        AssignableType<AndroidBuildSystem> androidBuildSystem;

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

            if (androidBuildSystem.Flag) {
                EditorUserBuildSettings.androidBuildSystem = androidBuildSystem;
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
            var cb = new ConfigTextBuilder();
            
            cb.Append("wsaSubtarget", wsaSubtarget);
            cb.Append("wsaUWPBuildType", wsaUWPBuildType);
            cb.Append("wsaUWPSDK", wsaUWPSDK);
            cb.Append("wsaBuildAndRunDeployTarget", wsaBuildAndRunDeployTarget);
            cb.Append("wsaGenerateReferenceProjects", wsaGenerateReferenceProjects);

            cb.Append("ps4BuildSubtarget", ps4BuildSubtarget);
            cb.Append("ps4HardwareTarget", ps4HardwareTarget);

            cb.Append("androidBuildSystem", androidBuildSystem);

            cb.Append("compressWithPsArc", compressWithPsArc);
            cb.Append("compressFilesInPackage", compressFilesInPackage);
            cb.Append("explicitNullChecks", explicitNullChecks);
            cb.Append("explicitDivideByZeroChecks", explicitDivideByZeroChecks);

            return cb.ToString();
        }

        public void Reload(AnyDictionary dict) {
            wsaSubtarget = AssignableType<WSASubtarget>.FromEnumDict(dict, "compressWithPsArc");
            wsaUWPBuildType = AssignableType<WSAUWPBuildType>.FromEnumDict(dict, "wsaUWPBuildType");
            wsaUWPSDK = AssignableType<string>.FromDict(dict, "wsaUWPSDK");
            wsaBuildAndRunDeployTarget = AssignableType<WSABuildAndRunDeployTarget>.FromEnumDict(dict, "wsaBuildAndRunDeployTarget");
            wsaGenerateReferenceProjects = AssignableType<bool>.FromDict(dict, "wsaGenerateReferenceProjects");

            ps4BuildSubtarget = AssignableType<PS4BuildSubtarget>.FromEnumDict(dict, "ps4BuildSubtarget");
            ps4HardwareTarget = AssignableType<PS4HardwareTarget>.FromEnumDict(dict, "ps4HardwareTarget");

            androidBuildSystem = AssignableType<AndroidBuildSystem>.FromEnumDict(dict, "androidBuildSystem");

            compressWithPsArc = AssignableType<bool>.FromDict(dict, "compressWithPsArc");
            compressFilesInPackage = AssignableType<bool>.FromDict(dict, "compressFilesInPackage");
            explicitNullChecks = AssignableType<bool>.FromDict(dict, "explicitNullChecks");
            explicitDivideByZeroChecks = AssignableType<bool>.FromDict(dict, "explicitDivideByZeroChecks");
        }

        internal static Modifier_EditorUserBuild Current() {
            return new Modifier_EditorUserBuild()
            {
                wsaSubtarget = AssignableType<WSASubtarget>.Create(EditorUserBuildSettings.wsaSubtarget),
                wsaUWPBuildType = AssignableType<WSAUWPBuildType>.Create(EditorUserBuildSettings.wsaUWPBuildType),
                wsaUWPSDK = AssignableType<string>.Create(EditorUserBuildSettings.wsaUWPSDK),
                wsaBuildAndRunDeployTarget = AssignableType<WSABuildAndRunDeployTarget>.Create(EditorUserBuildSettings.wsaBuildAndRunDeployTarget),
                wsaGenerateReferenceProjects = AssignableType<bool>.Create(EditorUserBuildSettings.wsaGenerateReferenceProjects),

                ps4BuildSubtarget = AssignableType<PS4BuildSubtarget>.Create(EditorUserBuildSettings.ps4BuildSubtarget),
                ps4HardwareTarget = AssignableType<PS4HardwareTarget>.Create(EditorUserBuildSettings.ps4HardwareTarget),

                androidBuildSystem = AssignableType<AndroidBuildSystem>.Create(EditorUserBuildSettings.androidBuildSystem),

                compressWithPsArc = AssignableType<bool>.Create(EditorUserBuildSettings.compressWithPsArc),
                compressFilesInPackage = AssignableType<bool>.Create(EditorUserBuildSettings.compressFilesInPackage),
                explicitNullChecks = AssignableType<bool>.Create(EditorUserBuildSettings.explicitNullChecks),
                explicitDivideByZeroChecks = AssignableType<bool>.Create(EditorUserBuildSettings.explicitDivideByZeroChecks),
            };
        }
    }
}
