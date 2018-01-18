using System.Collections.Generic;
using System.Text;

namespace Assets.Minamo.Editor {
    class Helper {
        internal static List<T> Convert<T>(List<object> l) {
            var retval = new List<T>();
            foreach(var el in l) {
                if(el == null) { continue; }
                if(typeof(T).IsAssignableFrom(el.GetType())) {
                    retval.Add((T)el);
                }
            }
            return retval;
        }
    }

    class ConfigTextBuilder {
        readonly StringBuilder sb = new StringBuilder();

        public void Append(string key, object value) {
            sb.AppendFormat("{0}={1}, ", key, value);
        }

        public void Append<T>(string key, AssignableType<T> value) {
            if (value.Flag) {
                sb.AppendFormat("{0}={1}, ", key, value);
            };
        }

        public override string ToString() {
            return sb.ToString();
        }
    }
}
