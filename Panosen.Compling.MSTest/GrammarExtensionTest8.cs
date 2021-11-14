using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panosen.Compling.Rules;

namespace Panosen.Compling.MSTest
{
    [TestClass]
    public class GrammarExtensionTest8
    {
        [TestMethod]
        public void TestMethod()
        {
            Grammar grammar = new Grammar();
            grammar.Rules = TestRules8.GetRules();

            var nonTerminals = grammar.NonTerminals;
            Assert.IsNotNull(nonTerminals);
            Assert.AreEqual(5, nonTerminals.Count);
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "S" }));
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "A" }));
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "B" }));
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "C" }));
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "D" }));

            #region First

            //FIRST(S) = {a,b,¦Å}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "S" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(3, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "b" }));
                Assert.IsTrue(firstSet.Contains(Symbols.Epsilon));
            }
            //FIRST(A) = {b,¦Å}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "A" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "b" }));
                Assert.IsTrue(firstSet.Contains(Symbols.Epsilon));
            }
            //FIRST(B) = {a,¦Å}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "B" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(firstSet.Contains(Symbols.Epsilon));
            }
            //FIRST(C) = {a,b,c}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "C" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(3, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "b" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
            }
            //FIRST(D) = {a,c}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "D" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
            }

            #endregion

            #region Follow

            //FOLLOW(S) = {$,)}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "S" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(1, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
            }
            //FOLLOW(A) = {$,)}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "A" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(3, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
            }
            //FOLLOW(B) = {+,$,)}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "B" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(1, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
            }
            //FOLLOW(C) = {+,$,)}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "C" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(1, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
            }
            //FOLLOW(D) = {*,+,$,)}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "D" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(1, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
            }

            #endregion
        }
    }
}
