using Microsoft.VisualStudio.TestTools.UnitTesting;
using Panosen.Compling.Rules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Panosen.Compling.LL1.MSTest
{
    [TestClass]
    public class GrammarExtensionTest9
    {
        [TestMethod]
        public void TestMethod()
        {
            Grammar grammar = new Grammar();
            grammar.Rules = new SampleRule9().GetRules();

            NewMethod(grammar);

            Run("grammar.IsLL1Grammar()", () =>
            {
                var isLL1Grammar = grammar.IsLL1Grammar();
                Assert.IsTrue(isLL1Grammar);
            });
        }

        private static void NewMethod(Grammar grammar)
        {
            //SELECT(0) = {}
            //SELECT(A→BCc)={a,b,c,d}
            Run("11", () =>
            {
                var selectSet = grammar.GetSelect(grammar.Rules[0]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(4, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "b" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
            });

            //SELECT(1) = {}
            //SELECT(A→gDB)={g}
            Run("12", () =>
            {
                var selectSet = grammar.GetSelect(grammar.Rules[1]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(1, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
            });

            //SELECT(2) = {}
            //SELECT(B→bCDE)={b}
            Run("13", () =>
            {
                var selectSet = grammar.GetSelect(grammar.Rules[2]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(1, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "b" }));
            });

            //SELECT(3) = {}
            //SELECT(B→ε)={a,c,d,g,f,#}
            Run("14", () =>
            {
                var selectSet = grammar.GetSelect(grammar.Rules[3]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(6, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "f" }));
                Assert.IsTrue(selectSet.Contains(Symbols.Dollar));
            });

            //SELECT(4) = {}
            //SELECT(C→DaB)={a,d}
            Run("15", () =>
            {
                var selectSet = grammar.GetSelect(grammar.Rules[4]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(2, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
            });

            //SELECT(5) = {}
            //SELECT(C→ca)={c}
            Run("16", () =>
            {
                var selectSet = grammar.GetSelect(grammar.Rules[5]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(1, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
            });

            //SELECT(6) = {}
            //SELECT(D→dD)={d}
            Run("17", () =>
            {
                var selectSet = grammar.GetSelect(grammar.Rules[6]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(1, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "d" }));
            });

            //SELECT(7) = {}
            //SELECT(D→ε)={a,b,c,g,f,#}
            Run("18", () =>
            {
                var selectSet = grammar.GetSelect(grammar.Rules[7]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(6, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "a" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "b" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "f" }));
                Assert.IsTrue(selectSet.Contains(Symbols.Dollar));
            });

            //SELECT(8) = {}
            //SELECT(E→gAf)={g}
            Run("19", () =>
            {
                var selectSet = grammar.GetSelect(grammar.Rules[8]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(1, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "g" }));
            });

            //SELECT(9) = {}
            //SELECT(E→c)={c}
            Run("20", () =>
            {
                var selectSet = grammar.GetSelect(grammar.Rules[9]);
                Assert.IsNotNull(selectSet);
                Assert.AreEqual(1, selectSet.Count);
                Assert.IsTrue(selectSet.Contains(new Symbol { Type = SymbolType.Terminal, Value = "c" }));
            });
        }

        private static void Run(string name, Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            action();

            stopwatch.Stop();

            var message = $"{name} = {stopwatch.ElapsedMilliseconds}ms";

            Console.WriteLine(message);
        }
    }
}
