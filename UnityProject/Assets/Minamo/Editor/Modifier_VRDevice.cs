using System.Text;
using UnityEditor;
using UnityEditorInternal.VR;

namespace Assets.Minamo.Editor {
    class Modifier_VRDevice : IModifier {
        const string DeviceOculus = "Oculus";
        const string DeviceOpenVR = "OpenVR";
        const string DeviceDaydream = "daydream";
        const string DeviceCardboard = "cardboard";

        readonly BuildTargetGroup targetGroup;
        bool enabled;
        string[] devices = new string[] { };
        StereoRenderingPath stereoRenderingPath;

        internal Modifier_VRDevice(BuildTargetGroup targetGroup) {
            this.targetGroup = targetGroup;
        }

        public void Reload(AnyDictionary dict) {
            enabled = dict.GetValue<bool>("enabled");

            var deviceListStr = dict.GetValue<string>("devices");
            this.devices = deviceListStr.Split(',');

            var table = StringEnumConverter.Get<StereoRenderingPath>();
            stereoRenderingPath = table[dict.GetValue<string>("stereoRenderingPath")];
        }

        public void Apply() {
            VREditor.SetVREnabledDevicesOnTargetGroup(targetGroup, devices);
            VREditor.SetVREnabledOnTargetGroup(targetGroup, enabled);
            PlayerSettings.stereoRenderingPath = stereoRenderingPath;
        }

        internal static Modifier_VRDevice Current(BuildTargetGroup g) {
            var devices = VREditor.GetVREnabledDevicesOnTargetGroup(g);
            var enabled = VREditor.GetVREnabledOnTargetGroup(g);
            return new Modifier_VRDevice(g)
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
