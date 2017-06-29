using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Minamo.Editor {
    public class AnyDictionaryTest {
        AnyDictionary CreateDict() {
            var dict = new Dictionary<string, object>()
            {
                {"str", "hello" },
                {"int", 123 },
                {"bool", true },
                {"dict", new Dictionary<string, object>() },
                {"list", new List<object>() },
            };
            return new AnyDictionary(dict);
        }

        [Test]
        public void Test_Count_Dict() {
            var d = new Dictionary<string, object>()
            {
                {"str", "hello" },
                {"int", 123 },
                {"null", null },
            };
            var dict = new AnyDictionary(d);
            Assert.AreEqual(3, dict.Count);
        }

        [Test]
        public void Test_Count_List() {
            var l = new List<object>
            {
                "hello",
                123,
                true,
                null,
            };
            var list = new AnyDictionary(l);
            Assert.AreEqual(4, list.Count);
        }

        [Test]
        public void Test_TryGetValue_NotExist() {
            var dict = CreateDict();
            string found;
            var ok = dict.TryGetValue("not-exist", out found);
            Assert.AreEqual(false, ok);
            Assert.AreEqual(null, found);
        }

        [Test]
        public void Test_TryGetValue_DiffType() {
            var dict = CreateDict();
            string found;
            var ok = dict.TryGetValue("int", out found);
            Assert.AreEqual(false, ok);
            Assert.AreEqual(null, found);
        }

        [Test]
        public void Test_TryGetValue_Success() {
            var dict = CreateDict();
            string found;
            var ok = dict.TryGetValue("str", out found);
            Assert.AreEqual(true, ok);
            Assert.AreEqual("hello", found);
        }

        [Test]
        public void Test_GetValue_string() {
            var dict = CreateDict();
            var invalidKeys = new string[]
            {
                "int", "bool", "dict", "list",
            };
            foreach(var k in invalidKeys) {
                Assert.AreEqual("", dict.GetValue<string>(k));
            }
            Assert.AreEqual("hello", dict.GetValue<string>("str"));
        }

        [Test]
        public void Test_GetValue_int() {
            var dict = CreateDict();
            var invalidKeys = new string[]
            {
                "str", "bool", "dict", "list",
            };
            foreach(var k in invalidKeys) {
                Assert.AreEqual(0, dict.GetValue<int>(k), "curr key: {0}", k);
            }
            Assert.AreEqual(123, dict.GetValue<int>("int"));
        }

        [Test]
        public void Test_GetValue_bool() {
            var dict = CreateDict();
            var invalidKeys = new string[]
            {
                "str", "int", "dict", "list"
            };
            foreach (var k in invalidKeys) {
                Assert.AreEqual(false, dict.GetValue<bool>(k));
            }
            Assert.AreEqual(true, dict.GetValue<bool>("bool"));
        }

        [Test]
        public void Test_GetDict() {
            var dict = CreateDict();
            var invalidKeys = new string[]
            {
                "str", "int", "bool", "list"
            };
            foreach(var k in invalidKeys) {
                Assert.IsNull(dict.GetDict(k));
            }
            Assert.IsNotNull(dict.GetDict("dict"));
        }

        [Test]
        public void Test_GetList() {
            var dict = CreateDict();
            var invalidKeys = new string[]
            {
                "str", "int", "bool", "dict"
            };
            foreach(var k in invalidKeys) {
                Assert.IsNull(dict.GetList(k));
            }
            Assert.IsNotNull(dict.GetList("list"));
        }

        [Test]
        public void Test_GetAt() {
            var l = new List<object>
            {
                "hello",
                123,
                true,
                null,
            };
            var list = new AnyDictionary(l);

            Assert.AreEqual("hello", list.GetAt<string>(0));
            Assert.AreEqual(123, list.GetAt<int>(1));
            Assert.AreEqual(true, list.GetAt<bool>(2));
            Assert.AreEqual(null, list.GetAt<string>(3));
        }
    }

}