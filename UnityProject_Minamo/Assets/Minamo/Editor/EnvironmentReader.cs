using System;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class EnvironmentReader {
        internal static bool TryRead(string key, out string val) {
            var parser = new Parser_String();
            try {
                var s = Environment.GetEnvironmentVariable(key);
                return parser.Parse(s, out val);

            } catch (ArgumentNullException e) {
                Debug.LogError(e.Message);
                val = "";
                return false;
            }
        }

        interface Parser<T> {
            bool Parse(string s, out T val);
        }

        class Parser_Int : Parser<int> {
            public bool Parse(string s, out int val) {
                if (s == null) {
                    val = 0;
                    return false;
                }

                try {
                    val = int.Parse(s);
                    return true;

                } catch (FormatException) {
                    val = 0;
                    return false;
                }
            }
        }

        class Parser_String : Parser<string> {
            public bool Parse(string s, out string val) {
                if (s == null) {
                    val = "";
                    return false;
                }

                val = s;
                return true;
            }
        }
    }
}
