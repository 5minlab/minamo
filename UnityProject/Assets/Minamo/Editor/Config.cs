using System.Collections.Generic;
using TinyJson;

namespace Assets.Minamo.Editor {
    public class Config {
        readonly AnyDictionary root;
        public AnyDictionary Root { get { return root; } }

        public AnyDictionary AndroidSDK
        {
            get { return new AnyDictionary(root.GetDict("androidSdk")); }
        }

        public AnyDictionary Identification
        {
            get { return new AnyDictionary(root.GetDict("identification")); }
        }

        public AnyDictionary VRDevices
        {
            get { return new AnyDictionary(root.GetDict("vrDevices")); }
        }

        public AnyDictionary Keystore
        {
            get { return new AnyDictionary(root.GetDict("keystore")); }
        }

        public AnyDictionary Build
        {
            get { return new AnyDictionary(root.GetDict("build")); }
        }

        public AnyDictionary Defines
        {
            get { return new AnyDictionary(root.GetList("defines")); }
        }

        public Config(string jsontext) {
            var d = jsontext.FromJson<object>() as Dictionary<string, object>;
            root = new AnyDictionary(d);
        }
    }
}
