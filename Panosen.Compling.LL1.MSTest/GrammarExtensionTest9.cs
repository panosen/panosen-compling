using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panosen.Compling.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling.LL1.MSTest
{
    [TestClass]
    public class GrammarExtensionTest9
    {
        [TestMethod]
        public void TestMethod()
        {
            Grammar grammar = new Grammar();
            grammar.Rules = new SampleRule9().GetRules();

            #region Select

            //SELECT(0) = {}
            //SELECT(A→BCc)={a,b,c,d}
            {
                var selectSet = grammar.GetSelect(grammar.Rules[0]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(4, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "b" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
            }

            //SELECT(1) = {}
            //SELECT(A→gDB)={g}
            {
                var selectSet = grammar.GetSelect(grammar.Rules[1]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(1, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
            }

            //SELECT(2) = {}
            //SELECT(B→bCDE)={b}
            {
                var selectSet = grammar.GetSelect(grammar.Rules[2]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(1, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "b" }));
            }

            //SELECT(3) = {}
            //SELECT(B→ε)={a,c,d,g,f,#}
            {
                var selectSet = grammar.GetSelect(grammar.Rules[3]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(6, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "f" }));
                Assert.IsTrue(selectSet.Contains(Symbols.Dollar));
            }

            //SELECT(4) = {}
            //SELECT(C→DaB)={a,d}
            {
                var selectSet = grammar.GetSelect(grammar.Rules[4]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(2, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
            }

            //SELECT(5) = {}
            //SELECT(C→ca)={c}
            {
                var selectSet = grammar.GetSelect(grammar.Rules[5]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(1, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
            }

            //SELECT(6) = {}
            //SELECT(D→dD)={d}
            {
                var selectSet = grammar.GetSelect(grammar.Rules[6]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(1, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
            }

            //SELECT(7) = {}
            //SELECT(D→ε)={a,b,c,g,f,#}
            {
                var selectSet = grammar.GetSelect(grammar.Rules[7]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(6, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "b" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "f" }));
                Assert.IsTrue(selectSet.Contains(Symbols.Dollar));
            }

            //SELECT(8) = {}
            //SELECT(E→gAf)={g}
            {
                var selectSet = grammar.GetSelect(grammar.Rules[8]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(1, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
            }

            //SELECT(9) = {}
            //SELECT(E→c)={c}
            {
                var selectSet = grammar.GetSelect(grammar.Rules[9]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(1, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
            }

            #endregion

            #region LL1

            var isLL1Grammar = grammar.IsLL1Grammar();
            Assert.IsTrue(isLL1Grammar);

            #endregion
        }
    }
}
