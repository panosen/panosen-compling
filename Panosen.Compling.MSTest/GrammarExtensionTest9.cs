using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panosen.Compling.Rules;

namespace Panosen.Compling.MSTest
{
    [TestClass]
    public class GrammarExtensionTest9
    {
        [TestMethod]
        public void TestMethod()
        {
            Grammar grammar = new Grammar();
            grammar.Rules = TestRules9.GetRules();

            var nonTerminals = grammar.NonTerminals;
            Assert.IsNotNull(nonTerminals);
            Assert.AreEqual(5, nonTerminals.Count);
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "A" }));
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "B" }));
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "C" }));
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "D" }));
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "E" }));

            #region First

            //FIRST(A) = {a,b,c,d,g}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "A" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(5, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "b" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
            }
            //FIRST(B) = {b,¦Å}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "B" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "b" }));
                Assert.IsTrue(firstSet.Contains(Symbols.Epsilon));
            }
            //FIRST(C) = {a,c,d}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "C" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(3, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
            }
            //FIRST(D) = {d,¦Å}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "D" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
                Assert.IsTrue(firstSet.Contains(Symbols.Epsilon));
            }
            //FIRST(E) = {c,g}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "E" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
            }

            #endregion

            #region Follow

            //FOLLOW(A) = {f, $}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "A" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(2, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "f" }));
            }
            //FOLLOW(B) = {a, c, d, g, f, $}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "B" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(6, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "f" }));
            }
            //FOLLOW(C) = {c, d, g}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "C" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(3, followSet.Count);
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
            }
            //FOLLOW(D) = {a, b, c, g, f, $}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "D" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(6, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "b" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "f" }));
            }
            //FOLLOW(E) = {a, c, d, g, f, $}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "E" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(6, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "f" }));
            }

            #endregion
        }
    }
}
