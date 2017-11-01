using System.Collections;
using System.Collections.Generic;

namespace Assets.Minamo.Editor {
    /// <summary>
    /// dictionary(key=string, value=enum) + default value
    /// </summary>
    class StringEnumDictionary<T> : IEnumerable<KeyValuePair<string, T>> {
        readonly Dictionary<string, T> table;
        readonly T defaultValue;

        internal StringEnumDictionary(Dictionary<string, T> dict, T defaultValue) {
            this.table = new Dictionary<string, T>(dict);
            this.defaultValue = defaultValue;
        }

        internal T this[string key]
        {
            get
            {
                T val;
                if (table.TryGetValue(key, out val)) {
                    return val;
                }
                return defaultValue;
            }
        }

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator() {
            return table.GetEnumerator();
        }

        internal bool MustGetValue(string key, out T val) {
            if (table.TryGetValue(key, out val)) {
                return true;
            }
            val = defaultValue;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return table.GetEnumerator();
        }
    }
}
