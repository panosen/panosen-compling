using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panosen.Compling.Rules;
using System.Collections.Generic;

namespace Panosen.Compling.MSTest
{
    [TestClass]
    public class GrammarExtensionTest7
    {
        [TestMethod]
        public void TestMethod()
        {

            Grammar grammar = new Grammar();
            grammar.Rules = new ProductionRuleCollection(new SampleRule7().GetRules()).ProductionRules;

            #region First

            var firstSetMap = FirstSetMapBuilder.BuildFirstSetMap(grammar);

            //FIRST(E) = {'(',id}
            {
                var firstSet = firstSetMap.GetHashSet(new Symbol { Type = SymbolType.NonTerminal, Value = "E" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "(" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "id" }));
            }
            //FIRST(M) = {+,¦Å}
            {
                var firstSet = firstSetMap.GetHashSet(new Symbol { Type = SymbolType.NonTerminal, Value = "M" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "+" }));
                Assert.IsTrue(firstSet.Contains(Symbols.Epsilon));
            }
            //FIRST(T) = {'(',id}
            {
                var firstSet = firstSetMap.GetHashSet(new Symbol { Type = SymbolType.NonTerminal, Value = "T" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "(" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "id" }));
            }
            //FIRST(N) = {*,¦Å}
            {
                var firstSet = firstSetMap.GetHashSet(new Symbol { Type = SymbolType.NonTerminal, Value = "N" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "*" }));
                Assert.IsTrue(firstSet.Contains(Symbols.Epsilon));
            }
            //FIRST(F) = {'(',id}
            {
                var firstSet = firstSetMap.GetHashSet(new Symbol { Type = SymbolType.NonTerminal, Value = "F" });
                Assert.IsNotNull(firstSet);
                Assert.AreEqual(2, firstSet.Count);
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "(" }));
                Assert.IsTrue(firstSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "id" }));
            }

            #endregion

            #region Follow

            var followSetMap = FollowSetMapBuilder.BuildFollowSetMap(grammar);

            //FOLLOW(E) = {$,)}
            {
                var followSet = followSetMap.GetHashSet(new Symbol { Type = SymbolType.NonTerminal, Value = "E" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(2, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = ")" }));
            }
            //FOLLOW(M) = {$,)}
            {
                var followSet = followSetMap.GetHashSet(new Symbol { Type = SymbolType.NonTerminal, Value = "M" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(2, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = ")" }));
            }
            //FOLLOW(T) = {+,$,)}
            {
                var followSet = followSetMap.GetHashSet(new Symbol { Type = SymbolType.NonTerminal, Value = "T" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(3, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "+" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = ")" }));
            }
            //FOLLOW(N) = {+,$,)}
            {
                var followSet = followSetMap.GetHashSet(new Symbol { Type = SymbolType.NonTerminal, Value = "N" });
                Assert.IsNotNull(followSet);
                Assert.AreEqual(3, followSet.Count);
                Assert.IsTrue(followSet.Contains(Symbols.Dollar));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "+" }));
                Assert.IsTrue(followSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = ")" }));
            }
            //FOLLOW(F) = {*,+,$,)}
            {
                var followSet = followSetMap.GetHashSet(new Symbol { Type = SymbolType.NonTerminal, Value = "F" });
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
