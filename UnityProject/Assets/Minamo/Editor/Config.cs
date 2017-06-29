using System.Collections.Generic;
using TinyJson;

namespace Assets.Minamo.Editor {
    public class Config {
        readonly Dictionary<string, object> root;
        public Dictionary<string, object> Root { get { return root; } }

        const string Key_AndroidSDK = "androidSdk";
        const string Key_Identification = "identification";
        const string Key_VRDevices = "vrDevices";
        const string Key_Keystore = "keystore";
        const string Key_Build = "build";
        const string Key_Defines = "defines";

        public Dictionary<string, int> AndroidSDK
        {
            get
            {
                Dictionary<string, int> dat;
                TryFindSubRoot(Key_AndroidSDK, out dat);
                return dat;
            }
        }

        public Dictionary<string, string> Identification
        {
            get
            {
                Dictionary<string, string> dat;
                TryFindSubRoot(Key_Identification, out dat);
                return dat;
            }
        }

        public Dictionary<string, string> VRDevices
        {
            get
            {
                Dictionary<string, string> dat;
                TryFindSubRoot(Key_VRDevices, out dat);
                return dat;
            }
        }

        public Dictionary<string, string> Keystore
        {
            get
            {
                Dictionary<string, string> dat;
                TryFindSubRoot(Key_Keystore, out dat);
                return dat;
            }
        }

        public Dictionary<string, object> Build
        {
            get
            {
                Dictionary<string, object> dat;
                if(TryFindSubRoot(Key_Build, out dat)) {
                    return dat;
                }
                return new Dictionary<string, object>();
            }
        }

        public string[] Defines
        {
            get
            {
                string[] dat;
                if(TryFindSubRoot(Key_Defines, out dat)) {
                    return dat;
                }
                return null;
            }
        }

        public Config(string jsontext) {
            root = jsontext.FromJson<object>() as Dictionary<string, object>;
        }

        bool TryFindSubRoot<T>(string key, out Dictionary<string, T> dat) {
            if(!root.ContainsKey(key)) {
                dat = null;
                return false;
            }

            var dict = root[key] as Dictionary<string, object>;
            if (dict == null) {
                dat = null;
                return false;
            }

            dat = new Dictionary<string, T>();
            foreach (var kv in dict) {
                if(typeof(T).IsAssignableFrom(kv.Value.GetType())) {
                    dat[kv.Key] = (T)kv.Value;
                }
            }
            return true;
        }

        bool TryFindSubRoot(string key, out string[] data) {
            if (!root.ContainsKey(key)) {
                data = null;
                return false;
            }

            var arr = root[key] as List<object>;
            if (arr == null) {
                data = null;
                return false;
            }

            var list = new List<string>();
            foreach(var el in arr) {
                if(el == null) { continue; }
                if(typeof(string).IsAssignableFrom(el.GetType())) {
                    list.Add((string)el);
                }
            }
            data = list.ToArray();
            return true;
        }
    }


}
