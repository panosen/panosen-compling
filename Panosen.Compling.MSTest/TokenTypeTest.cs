using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Panosen.Compling.MSTest
{
    [TestClass]
    public class TokenTypeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            TokenType empty = null;
            Assert.IsTrue(empty == null);
            Assert.IsTrue(null == empty);

            TokenType one = "A";
            TokenType two = "a";
            TokenType three = "c";

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

            Dictionary<TokenType, string> map = new Dictionary<TokenType, string>();
            map.Add("a", "a");
            TokenType key = "A";
            Assert.IsTrue(map.ContainsKey(key));
        }
    }
}
