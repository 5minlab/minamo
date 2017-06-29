using System.Text;
using UnityEditor;
using UnityEditorInternal.VR;

namespace Assets.Minamo.Editor {
    class VRDeviceModifier : IModifier {
        public const string DeviceOculus = "Oculus";
        public const string DeviceOpenVR = "OpenVR";
        public const string DeviceDaydream = "daydream";
        public const string DeviceCardboard = "cardboard";

        readonly BuildTargetGroup targetGroup;
        public bool enabled;
        string[] devices = new string[] { };

        public VRDeviceModifier(BuildTargetGroup targetGroup) {
            this.targetGroup = targetGroup;
        }

        public void Reload(AnyDictionary dict) {
            enabled = dict.GetValue<bool>("enabled");

            var deviceListStr = dict.GetValue<string>("devices");
            this.devices = deviceListStr.Split(',');
        }

        public void Apply() {
            VREditor.SetVREnabledDevicesOnTargetGroup(targetGroup, devices);
            VREditor.SetVREnabledOnTargetGroup(targetGroup, enabled);
        }

        public static VRDeviceModifier Current(BuildTargetGroup g) {
            var devices = VREditor.GetVREnabledDevicesOnTargetGroup(g);
            var enabled = VREditor.GetVREnabledOnTargetGroup(g);
            return new VRDeviceModifier(g)
            {
                devices = devices,
                enabled = enabled,
            };
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            sb.AppendFormat("enabled={0}, ", enabled);
            sb.AppendFormat("devices={0}", string.Join(",", devices));
            return sb.ToString();
        }
    }
}
