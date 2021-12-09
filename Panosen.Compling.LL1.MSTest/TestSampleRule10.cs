using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panosen.Compling.Rules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Panosen.Compling.LL1.MSTest
{
    /// <summary>
    /// id + id + id + id
    /// </summary>
    [TestClass]
    public class TestSampleRule10
    {
        [TestMethod]
        public void TestMethod()
        {
            Grammar grammar = new Grammar();
            grammar.Rules =new SampleRule10().GetRules();

            Assert.IsTrue(grammar.IsLL1Grammar());

            var tokenList = new List<Symbol>();
            tokenList.Add(new Symbol { Type = SymbolType.Terminal, Value = "id" });
            tokenList.Add(new Symbol { Type = SymbolType.Terminal, Value = "+" });
            tokenList.Add(new Symbol { Type = SymbolType.Terminal, Value = "id" });
            tokenList.Add(new Symbol { Type = SymbolType.Terminal, Value = "+" });
            tokenList.Add(new Symbol { Type = SymbolType.Terminal, Value = "id" });
            tokenList.Add(new Symbol { Type = SymbolType.Terminal, Value = "+" });
            tokenList.Add(new Symbol { Type = SymbolType.Terminal, Value = "id" });

            GrammarNode root;
            Symbol errorToken;

            var accept = LL1Analyser.Analyse(tokenList, out root, out errorToken, grammar);

            Assert.IsTrue(accept);
        }
    }
}
