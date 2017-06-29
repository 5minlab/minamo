using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Minamo.Editor {
    public class AnyDictionaryTest {
        AnyDictionary Create() {
            var dict = new Dictionary<string, object>()
            {
                {"str", "hello" },
                {"int", 1 },
                {"dict", new Dictionary<string, object>() },
                {"list", new List<object>() },
            };
            return new AnyDictionary(dict);
        }

        [Test]
        public void Test_TryGetValue_NotExist() {
            var dict = Create();
            string found;
            var ok = dict.TryGetValue("not-exist", out found);
            Assert.AreEqual(false, ok);
            Assert.AreEqual(null, found);
        }

        [Test]
        public void Test_TryGetValue_DiffType() {
            var dict = Create();
            string found;
            var ok = dict.TryGetValue("int", out found);
            Assert.AreEqual(false, ok);
            Assert.AreEqual(null, found);
        }

        [Test]
        public void Test_TryGetValue_Success() {
            var dict = Create();
            string found;
            var ok = dict.TryGetValue("str", out found);
            Assert.AreEqual(true, ok);
            Assert.AreEqual("hello", found);
        }

        [Test]
        public void Test_GetString() {
            var dict = Create();
            Assert.AreEqual("hello", dict.GetString("str"));
            Assert.AreEqual(null, dict.GetString("int"));
            Assert.AreEqual(null, dict.GetString("dict"));
            Assert.AreEqual(null, dict.GetString("list"));
        }

        [Test]
        public void Test_GetInt() {
            var dict = Create();
            Assert.AreEqual(0, dict.GetInt("str"));
            Assert.AreEqual(1, dict.GetInt("int"));
            Assert.AreEqual(0, dict.GetInt("dict"));
            Assert.AreEqual(0, dict.GetInt("list"));
        }

        [Test]
        public void Test_GetDict() {
            var dict = Create();
            Assert.IsNull(dict.GetDict("str"));
            Assert.IsNull(dict.GetDict("int"));
            Assert.IsNotNull(dict.GetDict("dict"));
            Assert.IsNull(dict.GetDict("list"));
        }

        [Test]
        public void Test_GetList() {
            var dict = Create();
            Assert.IsNull(dict.GetList("str"));
            Assert.IsNull(dict.GetList("int"));
            Assert.IsNull(dict.GetList("dict"));
            Assert.IsNotNull(dict.GetList("list"));
        }
    }

}