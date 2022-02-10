using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panosen.Compling.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling.MSTest
{
    [TestClass]
    public class GrammarTokenizer11Test
    {
        [TestMethod]
        public void TestMethod()
        {
            var rules = new SampleRule11().GetRules();

            //p { color : red ; }
            var source = new SampleRule11().GetSamples();

            var tokenCollection = new GrammarTokenizer(new ProductionRuleCollection(rules)).Analyze(source);

            Assert.IsNotNull(tokenCollection);
            Assert.AreEqual(7, tokenCollection.Count());

            Assert.AreEqual("p", tokenCollection.TokenList[0].Value);
            Assert.AreEqual("{", tokenCollection.TokenList[1].Value);
            Assert.AreEqual("color", tokenCollection.TokenList[2].Value);
            Assert.AreEqual(":", tokenCollection.TokenList[3].Value);
            Assert.AreEqual("red", tokenCollection.TokenList[4].Value);
            Assert.AreEqual(";", tokenCollection.TokenList[5].Value);
            Assert.AreEqual("}", tokenCollection.TokenList[6].Value);
        }
    }
}
