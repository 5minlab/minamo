using System;
using System.Collections.Generic;

namespace Assets.Minamo.Editor {
    class AnyDictionary {
        readonly Dictionary<string, object> dict;
        public AnyDictionary(Dictionary<string, object> dict) {
            this.dict = dict;
        }

        public bool ContainsKey(string k) {
            return dict.ContainsKey(k);
        }

        public bool TryGetValue<T>(string key, out T val) {
            object obj;
            if (dict.TryGetValue(key, out obj)) {
                if (typeof(T).IsAssignableFrom(obj.GetType())) {
                    val = (T)Convert.ChangeType(obj, typeof(T));
                    return (val != null);
                }
            }
            val = default(T);
            return false;
        }

        public string GetString(string key) {
            string s;
            TryGetValue(key, out s);
            return s;
        }

        public int GetInt(string key) {
            int v;
            TryGetValue(key, out v);
            return v;
        }

        public Dictionary<string, object> GetDict(string key) {
            Dictionary<string, object> d;
            TryGetValue(key, out d);
            return d;
        }

        public List<object> GetList(string key) {
            List<object> l;
            TryGetValue(key, out l);
            return l;
        }
    }
}
