using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class AnyDictionary {
        readonly Dictionary<string, object> dict;
        readonly List<object> list;

        internal AnyDictionary(object obj) {
            if(obj == null) {
                dict = new Dictionary<string, object>();
                list = new List<object>();

            } else if(typeof(Dictionary<string, object>) == obj.GetType()) {
                this.dict = obj as Dictionary<string, object>;
                this.list = null;

            } else if(typeof(List<object>) == obj.GetType()) {
                this.dict = null;
                this.list = obj as List<object>;

            } else {
                Debug.Assert(false, "cannot create any dictionary");
            }
        }

        internal int Count
        {
            get
            {
                if(dict != null) {
                    return dict.Count;
                }
                if(list != null) {
                    return list.Count;
                }
                return 0;
            }
        }

        internal T GetAt<T>(int idx) {
            if(list == null) {
                return default(T);
            }
            if(idx < 0) {
                return default(T);
            }
            if(idx >= Count) {
                return default(T);
            }
            var obj = list[idx];
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        internal bool ContainsKey(string k) {
            return dict.ContainsKey(k);
        }

        internal bool TryGetValue<T>(string key, out T val, T defaultVal = default(T)) {
            if(dict == null) {
                val = defaultVal;
                return false;
            }

            object obj;
            if (dict.TryGetValue(key, out obj)) {
                if (typeof(T).IsAssignableFrom(obj.GetType())) {
                    val = (T)Convert.ChangeType(obj, typeof(T));
                    return (val != null);
                }
            }
            val = defaultVal;
            return false;
        }

        internal T GetValue<T>(string key, T defaultVal = default(T)) {
            if(typeof(T) == typeof(string)) {
                var v = (string)Convert.ChangeType(defaultVal, typeof(string));
                return (T)Convert.ChangeType(GetString(key, v), typeof(string));
            }
            if(typeof(T) == typeof(bool)) {
                var v = (bool)Convert.ChangeType(defaultVal, typeof(bool));
                return (T)Convert.ChangeType(GetBool(key, v), typeof(bool));
            }
            if(typeof(T) == typeof(int)) {
                var v = (int)Convert.ChangeType(defaultVal, typeof(int));
                return (T)Convert.ChangeType(GetInt(key, v), typeof(int));
            }
            return defaultVal;
        }

        /// <summary>
        /// null string을 취급하면 널체크가 귀찮다
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetString(string key, string defaultVal) {
            string s;
            var ok = TryGetValue(key, out s);
            if(!ok) {
                return defaultVal;
            }
            if(s == null) {
                return "";
            }
            return s;
        }

        int GetInt(string key, int defaultVal) {
            int v;
            var ok = TryGetValue(key, out v);
            if(!ok) {
                return defaultVal;
            }
            return v;
        }

        bool GetBool(string key, bool defaultVal) {
            bool v;
            var ok = TryGetValue(key, out v);
            if(!ok) {
                return defaultVal;
            }
            return v;
        }

        internal Dictionary<string, object> GetDict(string key) {
            Dictionary<string, object> d;
            TryGetValue(key, out d);
            return d;
        }

        internal List<object> GetList(string key) {
            List<object> l;
            TryGetValue(key, out l);
            return l;
        }
    }
}
