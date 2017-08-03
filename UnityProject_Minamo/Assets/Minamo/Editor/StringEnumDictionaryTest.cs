using NUnit.Framework;
using UnityEditor;

namespace Assets.Minamo.Editor {
    class StringEnumDictionaryTest {
        [Test]
        public void MustGetValueTest() {
            var dict = StringEnumConverter.Get<ScriptingImplementation>();

            ScriptingImplementation val = ScriptingImplementation.Mono2x;
            bool ok = false;

            // blank
            ok = dict.MustGetValue("", out val);
            Assert.AreEqual(false, ok);

            // exist
            ok = dict.MustGetValue("Mono2x", out val);
            Assert.AreEqual(true, ok);
            Assert.AreEqual(ScriptingImplementation.Mono2x, val);

            // not exist key
            ok = dict.MustGetValue("invalid", out val);
            Assert.AreEqual(false, ok);
        }
    }
}
