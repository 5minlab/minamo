using System.Collections.Generic;

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
}
