using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            };
        }
    }
}
