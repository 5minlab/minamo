using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Minamo.Editor {
    public class AnyDictionary {
        readonly Dictionary<string, object> dict;
        readonly List<object> list;

        public AnyDictionary(object obj) {
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

        public int Count
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

        public T GetAt<T>(int idx) {
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

        public bool ContainsKey(string k) {
            return dict.ContainsKey(k);
        }

        public bool TryGetValue<T>(string key, out T val) {
            if(dict == null) {
                val = default(T);
                return false;
            }

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

        public T GetValue<T>(string key) {
            if(typeof(T) == typeof(string)) {
                return (T)Convert.ChangeType(GetString(key), typeof(string));
            }
            if(typeof(T) == typeof(bool)) {
                return (T)Convert.ChangeType(GetBool(key), typeof(bool));
            }
            if(typeof(T) == typeof(int)) {
                return (T)Convert.ChangeType(GetInt(key), typeof(int));
            }

            return default(T);
        }

        /// <summary>
        /// null string을 취급하면 널체크가 귀찮다
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetString(string key) {
            string s;
            TryGetValue(key, out s);
            if(s == null) {
                return "";
            }
            return s;
        }

        int GetInt(string key) {
            int v;
            TryGetValue(key, out v);
            return v;
        }

        bool GetBool(string key) {
            bool v;
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
