using System.Collections.Generic;
using TinyJson;
using UnityEditor;

namespace Assets.Minamo.Editor {
    internal class Config {
        readonly AnyDictionary root;
        internal AnyDictionary Root { get { return root; } }

        AnyDictionary AndroidSDK
        {
            get { return new AnyDictionary(root.GetDict("androidSdk")); }
        }

        AnyDictionary Identification
        {
            get { return new AnyDictionary(root.GetDict("identification")); }
        }

        AnyDictionary VRDevices
        {
            get { return new AnyDictionary(root.GetDict("vrDevices")); }
        }

        AnyDictionary Keystore
        {
            get { return new AnyDictionary(root.GetDict("keystore")); }
        }

        AnyDictionary Build
        {
            get { return new AnyDictionary(root.GetDict("build")); }
        }

        AnyDictionary Defines
        {
            get { return new AnyDictionary(root.GetList("defines")); }
        }

        AnyDictionary Publishing
        {
            get { return new AnyDictionary(root.GetDict("publishing")); }
        }

        internal Config(string jsontext) {
            var d = jsontext.FromJson<object>() as Dictionary<string, object>;
            root = new AnyDictionary(d);
        }

        internal PlayerBuildExecutor PlayerBuild
        {
            get { return new PlayerBuildExecutor(Build); }
        }

        internal BuildTarget BuildTarget
        {
            get { return PlayerBuild.Target; }
        }

        internal BuildTargetGroup BuildTargetGroup
        {
            get { return PlayerBuild.TargetGroup; }
        }


        internal class ModifierTuple {
            internal IModifier modifier;
            internal AnyDictionary data;

            internal ModifierTuple(IModifier m, AnyDictionary d) {
                this.modifier = m;
                this.data = d;
            }
        }

        ModifierTuple[] CreateConfigBased(BuildTargetGroup targetGroup) {
            return new ModifierTuple[]
            {
                new ModifierTuple(new Modifier_AndroidSdkVersion(), AndroidSDK),
                new ModifierTuple(new Modifier_Identification(targetGroup), Identification),
                new ModifierTuple(new Modifier_VRDevice(targetGroup), VRDevices),
                new ModifierTuple(new Modifier_Keystore(), Keystore),
                new ModifierTuple(new Modifier_DefineSymbol(targetGroup), Defines),
                new ModifierTuple(new Modifier_Publishing(), Publishing),
            };
        }

        ModifierTuple[] CreateCurrentBased(BuildTargetGroup targetGroup) {
            return new ModifierTuple[]
            {
                new ModifierTuple(Modifier_AndroidSdkVersion.Current(), AndroidSDK),
                new ModifierTuple(Modifier_Identification.Current(targetGroup), Identification),
                new ModifierTuple(Modifier_VRDevice.Current(targetGroup), VRDevices),
                new ModifierTuple(Modifier_Keystore.Current(), Keystore),
                new ModifierTuple(Modifier_DefineSymbol.Current(targetGroup), Defines),
                new ModifierTuple(Modifier_Publishing.Current(), Publishing),
            };
        }

        internal IModifier[] CreateConfigModifiers() {
            var targetGroup = BuildTargetGroup;
            var modifiers = new List<IModifier>();
            foreach(var t in CreateConfigBased(targetGroup)) {
                if (t.data.Count > 0) {
                    t.modifier.Reload(t.data);
                    modifiers.Add(t.modifier);
                }
            }
            return modifiers.ToArray();
        }

        internal IModifier[] CreateCurrentModifiers() {
            var targetGroup = BuildTargetGroup;
            var modifiers = new List<IModifier>();
            foreach (var t in CreateCurrentBased(targetGroup)) {
                if (t.data.Count > 0) {
                    modifiers.Add(t.modifier);
                }
            }
            return modifiers.ToArray();
        }
    }
}
