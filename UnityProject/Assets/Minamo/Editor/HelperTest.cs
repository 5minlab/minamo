using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Minamo.Editor {
    class HelperTest {
        [Test]
        void TestConvert() {
            var o = new List<object>()
            {
                1, "a", null,
            };

            var intlist = Helper.Convert<int>(o);
            Assert.AreEqual(intlist, new List<int>(){ 1 });

            var strlist = Helper.Convert<string>(o);
            Assert.AreEqual(strlist, new List<string>(){ "a" });
        }
    }
}
