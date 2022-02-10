using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panosen.Compling.Rules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Panosen.Compling.LL1.MSTest
{
    [TestClass]
    public class TestSampleRule12
    {
        [TestMethod]
        public void TestMethod()
        {
            Grammar grammar = new Grammar();
            grammar.Rules = new ProductionRuleCollection(new SampleRule12().GetRules()).ProductionRules;

            Assert.IsTrue(grammar.IsLL1Grammar());

            var tokenList = new TokenCollection();
            tokenList.AddToken("p");
            tokenList.AddToken("{");
            tokenList.AddToken("c");
            tokenList.AddToken("o");
            tokenList.AddToken("l");
            tokenList.AddToken("o");
            tokenList.AddToken("r");
            tokenList.AddToken(":");
            tokenList.AddToken("r");
            tokenList.AddToken("e");
            tokenList.AddToken("d");
            tokenList.AddToken(";");
            tokenList.AddToken("}");

            tokenList = new StandardTokenizer().Analyze("p { color: red;}");

            GrammarNode root;
            Token errorToken;

            var accept = LL1Analyser.Analyse(tokenList, out root, out errorToken, grammar);

            Assert.IsTrue(accept);
        }
    }
}
