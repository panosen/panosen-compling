using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Panosen.Compling.SLR1.MSTest
{
    [TestClass]
    public class TheStateTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            TheState empty = null;
            Assert.IsTrue(empty == null);
            Assert.IsTrue(null == empty);

            TheState one = "A";
            TheState two = "a";
            TheState three = "c";

            Assert.IsTrue(one.Equals(two));
            Assert.IsTrue(two.Equals(one));

            Assert.IsTrue(one == two);
            Assert.IsTrue(two == one);

            Assert.IsFalse(one == empty);
            Assert.IsFalse(empty == one);

            Assert.IsTrue(empty != one);
            Assert.IsTrue(one != empty);

            Assert.IsTrue(one != three);
            Assert.IsTrue(three != one);

            Dictionary<TheState, string> map = new Dictionary<TheState, string>();
            map.Add("a", "a");
            TheState key = "a";
            Assert.IsTrue(map.ContainsKey(key));
        }
    }
}
