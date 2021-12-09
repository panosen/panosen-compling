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
    public class TestSampleRule7
    {

        [TestMethod]
        public void TestMethod()
        {
            Grammar grammar = new Grammar();
            grammar.Rules = new SampleRule7().GetRules();

            Assert.IsTrue(grammar.IsLL1Grammar());

            var tokenList = new List<Symbol>();
            tokenList.Add(new Symbol { Type = SymbolType.Terminal, Value = "(" });
            tokenList.Add(new Symbol { Type = SymbolType.Terminal, Value = "id" });
            tokenList.Add(new Symbol { Type = SymbolType.Terminal, Value = ")" });

            GrammarNode root;
            Symbol errorToken;

            var accept = LL1Analyser.Analyse(tokenList, out root, out errorToken, grammar);

            //JsonSerializerSettings settings = new JsonSerializerSettings();
            //settings.NullValueHandling = NullValueHandling.Ignore;
            //settings.Converters.Add(new TINYNodeConvertor());

            Assert.IsTrue(accept);
        }
    }
}
