using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Panosen.Compling.MSTest
{
    [TestClass]
    public class StandardTokenizerTest
    {
        [TestMethod]
        public void TestParse()
        {
            var text = "(a|b)*abb";

            var tokenCollection = new StandardTokenizer().Analyze(text);

            Assert.IsNotNull(tokenCollection.TokenList);
            Assert.AreEqual(9, tokenCollection.TokenList.Count);

            {
                var token = tokenCollection.TokenList[0];
                Assert.AreEqual("(", token.Value);
            }
            {
                var token = tokenCollection.TokenList[1];
                Assert.AreEqual("a", token.Value);
            }
            {
                var token = tokenCollection.TokenList[2];
                Assert.AreEqual("|", token.Value);
            }
            {
                var token = tokenCollection.TokenList[3];
                Assert.AreEqual("b", token.Value);
            }
            {
                var token = tokenCollection.TokenList[4];
                Assert.AreEqual(")", token.Value);
            }
            {
                var token = tokenCollection.TokenList[5];
                Assert.AreEqual("*", token.Value);
            }
            {
                var token = tokenCollection.TokenList[6];
                Assert.AreEqual("a", token.Value);
            }
            {
                var token = tokenCollection.TokenList[7];
                Assert.AreEqual("b", token.Value);
            }
            {
                var token = tokenCollection.TokenList[8];
                Assert.AreEqual("b", token.Value);
            }
        }
    }
}
