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

            var tokenList = new TokenCollection();
            tokenList.AddToken("(");
            tokenList.AddToken("id");
            tokenList.AddToken(")");

            GrammarNode root;
            Token errorToken;

            var accept = LL1Analyser.Analyse(tokenList, out root, out errorToken, grammar);

            //JsonSerializerSettings settings = new JsonSerializerSettings();
            //settings.NullValueHandling = NullValueHandling.Ignore;
            //settings.Converters.Add(new TINYNodeConvertor());

            Assert.IsTrue(accept);
        }
    }
}
