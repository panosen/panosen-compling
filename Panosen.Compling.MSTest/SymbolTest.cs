using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Panosen.Compling.MSTest
{
    [TestClass]
    public class SymbolTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Symbol empty = null;
            Assert.IsTrue(empty == null);
            Assert.IsTrue(null == empty);

            Symbol upperA1 = new Symbol { Type = SymbolType.NonTerminal, Value = "A" };
            Symbol upperA2 = new Symbol { Type = SymbolType.NonTerminal, Value = "A" };
            Symbol lowerA = new Symbol { Type = SymbolType.NonTerminal, Value = "a" };
            Symbol upperB = new Symbol { Type = SymbolType.NonTerminal, Value = "B" };

            var upperA1Hash = upperA1.GetHashCode();
            var upperA2Hash = upperA2.GetHashCode();
            Assert.AreEqual(upperA1Hash, upperA2Hash);

            Assert.IsTrue(upperA1.Equals(upperA2));
            Assert.IsTrue(upperA2.Equals(upperA1));

            Assert.IsTrue(upperA1 == upperA2);
            Assert.IsTrue(upperA2 == upperA1);

            Assert.IsFalse(upperA1 == empty);
            Assert.IsFalse(empty == upperA1);

            Assert.IsTrue(empty != upperA1);
            Assert.IsTrue(upperA1 != empty);

            Assert.IsTrue(upperA1 != upperB);
            Assert.IsTrue(upperB != upperA1);

            Assert.IsTrue(upperA1 != lowerA);
            Assert.IsTrue(lowerA != upperA1);

            Dictionary<Symbol, string> map = new Dictionary<Symbol, string>();
            map.Add(upperA1, "a");
            Assert.IsTrue(map.ContainsKey(upperA2));
        }
    }
}
