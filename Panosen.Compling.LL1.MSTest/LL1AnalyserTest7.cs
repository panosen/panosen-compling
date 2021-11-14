using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Panosen.Compling.Rules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Panosen.Compling.LL1.MSTest
{
    [TestClass]
    public class LL1AnalyserTest7
    {
        [TestMethod]
        public void TestMethod()
        {
            Grammar grammar = new Grammar();
            grammar.Rules = TestRules7.GetRules();

            var tokenList = new List<Symbol>();
            tokenList.Add(new Symbol { Type = SymbolType.Terminal, Value = "(" });
            tokenList.Add(new Symbol { Type = SymbolType.Terminal, Value = "id" });
            tokenList.Add(new Symbol { Type = SymbolType.Terminal, Value = ")" });

            TINYNode root;
            Symbol errorToken;

            var accept = LL1Analyser.Analyse(tokenList, out root, out errorToken, grammar);

            File.WriteAllText(@"g:\1.json", JsonConvert.SerializeObject(root, Formatting.Indented));

            Assert.IsTrue(accept);
        }
    }
}
