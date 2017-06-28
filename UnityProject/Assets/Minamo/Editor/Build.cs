using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Minamo.Editor {
    public class Build {
        public static void Execute() {
            string configFilePath;
            if(!EnvironmentReader.TryRead("CONFIG_PATH", out configFilePath)) {
                Debug.Log("cannot find CONFIG_PATH");
                return;
            }

            string outputFilePath;
            if(!EnvironmentReader.TryRead("OUTPUT_PATH", out outputFilePath)) {
                Debug.Log("cannot find OUTPUT_PATH");
                return;
            }

            ExecuteCommon(configFilePath, outputFilePath);
        }

        public static void ExecuteCommon(string configFilePath, string outputFilePath) {
            var content = File.ReadAllText(configFilePath);
            var config = new Config(content);

            IModifier[] currModifiers;
            IModifier[] nextModifiers;
            CreateModifiers(config, out currModifiers, out nextModifiers);

            foreach (var m in nextModifiers) {
                m.Apply();
            }

            var executor = new PlayerBuildExecutor(config.Build);
            executor.Build(outputFilePath);

            // restore
            foreach (var m in currModifiers) {
                m.Apply();
            }
        }


        public static void CreateModifiers(Config config, out IModifier[] currs, out IModifier[] nexts) {
            var currModifiers = new List<IModifier>();
            var nextModifiers = new List<IModifier>();

            var executor = new PlayerBuildExecutor(config.Build);
            var targetGroup = executor.TargetGroup;

            if (config.AndroidSDK != null) {
                var m = new AndroidSdkVersionModifier(config.AndroidSDK);
                currModifiers.Add(AndroidSdkVersionModifier.Current());
                nextModifiers.Add(m);
            }

            if(config.Identification != null) {
                var m = new IdentificationModifier(targetGroup, config.Identification);
                currModifiers.Add(IdentificationModifier.Current(targetGroup));
                nextModifiers.Add(m);
            }


            if(config.VRDevices != null) {
                var m = new VRDeviceModifier(targetGroup, config.VRDevices);
                currModifiers.Add(VRDeviceModifier.Current(targetGroup));
                nextModifiers.Add(m);
            }

            if(config.Keystore != null) {
                var m = new KeystoreModifier(config.Keystore);
                currModifiers.Add(KeystoreModifier.Current());
                nextModifiers.Add(m);
            }

            if(config.Defines != null) {
                var m = new DefineSymbolModifier(targetGroup, config.Defines);
                currModifiers.Add(DefineSymbolModifier.Current(targetGroup));
                nextModifiers.Add(m);
            }


            currs = currModifiers.ToArray();
            nexts = nextModifiers.ToArray();
        }
    }
}
