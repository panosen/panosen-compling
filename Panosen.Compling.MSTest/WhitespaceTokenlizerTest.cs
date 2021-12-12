using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling.MSTest
{
    [TestClass]
    public class WhitespaceTokenlizerTest
    {
        [TestMethod]
        public void TestMethod()
        {
            var text = "  zhang san  de hao 123. ok";

            var tokenCollection = new WhitespaceTokenizer().Analyze(text);

            Assert.IsNotNull(tokenCollection.TokenList);
            Assert.AreEqual(6, tokenCollection.TokenList.Count);

            Assert.AreEqual("zhang", tokenCollection.TokenList[0].Value);
            Assert.AreEqual("san", tokenCollection.TokenList[1].Value);
            Assert.AreEqual("de", tokenCollection.TokenList[2].Value);
            Assert.AreEqual("hao", tokenCollection.TokenList[3].Value);
            Assert.AreEqual("123.", tokenCollection.TokenList[4].Value);
            Assert.AreEqual("ok", tokenCollection.TokenList[5].Value);
        }
    }
}
