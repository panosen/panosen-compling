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

            {
                var token = tokenCollection.TokenList[0];
                Assert.AreEqual("zhang", token.Value);
                Assert.AreEqual(1, token.Row);
                Assert.AreEqual(3, token.Col);
            }
            {
                var token = tokenCollection.TokenList[1];
                Assert.AreEqual("san", token.Value);
                Assert.AreEqual(1, token.Row);
                Assert.AreEqual(9, token.Col);
            }
            {
                var token = tokenCollection.TokenList[2];
                Assert.AreEqual("de", token.Value);
                Assert.AreEqual(1, token.Row);
                Assert.AreEqual(14, token.Col);
            }
            {
                var token = tokenCollection.TokenList[3];
                Assert.AreEqual("hao", token.Value);
                Assert.AreEqual(1, token.Row);
                Assert.AreEqual(17, token.Col);
            }
            {
                var token = tokenCollection.TokenList[4];
                Assert.AreEqual("123.", token.Value);
                Assert.AreEqual(1, token.Row);
                Assert.AreEqual(21, token.Col);
            }
            {
                var token = tokenCollection.TokenList[5];
                Assert.AreEqual("ok", token.Value);
                Assert.AreEqual(1, token.Row);
                Assert.AreEqual(26, token.Col);
            }

        }
    }
}
