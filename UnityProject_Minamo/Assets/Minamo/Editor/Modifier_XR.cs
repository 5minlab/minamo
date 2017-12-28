using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditorInternal.VR;

namespace Assets.Minamo.Editor {
    class Modifier_XR : IModifier {
        const string DeviceOculus = "Oculus";
        const string DeviceOpenVR = "OpenVR";
        const string DeviceDaydream = "daydream";
        const string DeviceCardboard = "cardboard";
        const string DeviceWindowsMR = "WindowsMR";
        const string DevicePlayStationVR = "PlayStationVR";

        readonly BuildTargetGroup targetGroup;
        bool enabled;
        string[] devices = new string[] { };
        StereoRenderingPath stereoRenderingPath;

        internal Modifier_XR(BuildTargetGroup targetGroup) {
            this.targetGroup = targetGroup;
        }

        public void Reload(AnyDictionary dict) {
            enabled = dict.GetValue<bool>("enabled");

            var rawDeviceList = dict.GetList("devices");
            var deviceList = new List<string>();
            for(var i = 0; i < rawDeviceList.Count;i++) {
                var el = rawDeviceList[i] as string;
                if(el != null) {
                    deviceList.Add(el);
                }
            }
            this.devices = deviceList.ToArray();

            var table = StringEnumConverter.Get<StereoRenderingPath>();
            stereoRenderingPath = table[dict.GetValue<string>("stereoRenderingPath")];
        }

        public void Apply() {
            VREditor.SetVREnabledDevicesOnTargetGroup(targetGroup, devices);
            VREditor.SetVREnabledOnTargetGroup(targetGroup, enabled);
            PlayerSettings.stereoRenderingPath = stereoRenderingPath;
        }

        internal static Modifier_XR Current(BuildTargetGroup g) {
            var devices = VREditor.GetVREnabledDevicesOnTargetGroup(g);
            var enabled = VREditor.GetVREnabledOnTargetGroup(g);
            return new Modifier_XR(g)
            {
                devices = devices,
                enabled = enabled,
                stereoRenderingPath = PlayerSettings.stereoRenderingPath,
            };
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            sb.AppendFormat("enabled={0}, ", enabled);
            sb.AppendFormat("devices={0}, ", string.Join(",", devices));
            sb.AppendFormat("stereoRenderingPath={0}", stereoRenderingPath);
            return sb.ToString();
        }
    }
}
