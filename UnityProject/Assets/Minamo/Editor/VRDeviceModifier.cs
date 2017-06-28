using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditorInternal.VR;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class VRDeviceModifier : IModifier {
        public const string KeyDevices = "devices";
        public const string KeyEnabled = "enabled";

        public const string DeviceOculus = "Oculus";
        public const string DeviceOpenVR = "OpenVR";
        public const string DeviceDaydream = "daydream";
        public const string DeviceCardboard = "cardboard";

        public bool enabled;
        BuildTargetGroup targetGroup;
        string[] devices = new string[] { };

        public VRDeviceModifier() { }
        public VRDeviceModifier(BuildTargetGroup targetGroup, IDictionary<string, string> map) {
            this.targetGroup = targetGroup;

            string enabledStr;
            if (map.TryGetValue(KeyEnabled, out enabledStr)) {
                var s = enabledStr.ToLower();
                if (s == "true") {
                    enabled = true;
                } else {
                    enabled = false;
                }
            } else {
                enabled = false;
            }


            if (enabled) {
                string deviceListStr;
                if (!map.TryGetValue(KeyDevices, out deviceListStr)) {
                    Debug.LogFormat("cannot find key : {0}", KeyDevices);
                }
                this.devices = deviceListStr.Split(',');
            }
        }

        public void Apply() {
            VREditor.SetVREnabledDevicesOnTargetGroup(targetGroup, devices);
            VREditor.SetVREnabledOnTargetGroup(targetGroup, enabled);
        }

        public static VRDeviceModifier Current(BuildTargetGroup g) {
            var devices = VREditor.GetVREnabledDevicesOnTargetGroup(g);
            var enabled = VREditor.GetVREnabledOnTargetGroup(g);
            return new VRDeviceModifier()
            {
                targetGroup = g,
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
