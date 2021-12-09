﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panosen.Compling.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling.LL1.MSTest
{
    [TestClass]
    public class TestSampleRule5
    {
        [TestMethod]
        public void TestMethod()
        {
            Grammar grammar = new Grammar();
            grammar.Rules = new SampleRule5().GetRules();

            Assert.IsTrue(grammar.IsLL1Grammar());
        }
    }
}
