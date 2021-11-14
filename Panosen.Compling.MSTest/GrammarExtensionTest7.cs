using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panosen.Compling.Rules;

namespace Panosen.Compling.MSTest
{
    [TestClass]
    public class GrammarExtensionTest7
    {
        [TestMethod]
        public void TestMethod()
        {

            Grammar grammar = new Grammar();
            grammar.Rules = TestRules7.GetRules();

            var nonTerminals = grammar.NonTerminals;
            Assert.IsNotNull(nonTerminals);
            Assert.AreEqual(5, nonTerminals.Count);
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "E" }));
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "M" }));
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "T" }));
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "N" }));
            Assert.IsTrue(nonTerminals.Contains(new Symbol { Type = SymbolType.NonTerminal, Value = "F" }));

            #region First

            //FIRST(E) = {'(',id}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "E" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "(" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "id" }));
            }
            //FIRST(M) = {+,¦Å}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "M" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "+" }));
                Assert.IsTrue(firstSet.Contains(Symbols.Epsilon));
            }
            //FIRST(T) = {'(',id}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "T" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "(" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "id" }));
            }
            //FIRST(N) = {*,¦Å}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "N" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "*" }));
                Assert.IsTrue(firstSet.Contains(Symbols.Epsilon));
            }
            //FIRST(F) = {'(',id}
            {
                var firstSet = grammar.GetFirstSet(new Symbol { Type = SymbolType.NonTerminal, Value = "F" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "(" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "id" }));
            }

            #endregion

            #region Follow

            //FOLLOW(E) = {$,)}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "E" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(2, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = ")" }));
            }
            //FOLLOW(M) = {$,)}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "M" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(2, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = ")" }));
            }
            //FOLLOW(T) = {+,$,)}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "T" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(3, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "+" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = ")" }));
            }
            //FOLLOW(N) = {+,$,)}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "N" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(3, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "+" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = ")" }));
            }
            //FOLLOW(F) = {*,+,$,)}
            {
                var followSet = grammar.GetFollowSet(new Symbol { Type = SymbolType.NonTerminal, Value = "F" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(4, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "*" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "+" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = ")" }));
            }

            #endregion
        }
    }
}
