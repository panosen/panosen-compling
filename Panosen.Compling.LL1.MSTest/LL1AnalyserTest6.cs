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
    public class LL1AnalyserTest6
    {
        [TestMethod]
        public void TestMethod()
        {
            Grammar grammar = new Grammar();
            grammar.Rules = TestRules6.GetRules();

            var isLL1Grammar = grammar.IsLL1Grammar();
            Assert.IsFalse(isLL1Grammar);
        }
    }
}
