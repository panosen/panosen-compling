using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public static class TestRules8
    {
        /*
S→AB
S→bC
A→ε
A→b
B→ε
B→aD
C→AD
C→b
D→aS
D→c
    */

        public static List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("S", "A", "B"));
            rules.Add(ToRule("S", "b", "C"));
            rules.Add(ToRule("A", "[Null]"));
            rules.Add(ToRule("A", "b"));
            rules.Add(ToRule("B", "[Null]"));
            rules.Add(ToRule("B", "a", "D"));
            rules.Add(ToRule("C", "A", "D"));
            rules.Add(ToRule("C", "b"));
            rules.Add(ToRule("D", "a", "S"));
            rules.Add(ToRule("D", "c"));

            return rules;
        }

        private static ProductionRule ToRule(params string[] items)
        {
            ProductionRule theRule = new ProductionRule();
            theRule.Left = new Symbol { Value = items[0], Type = GetSymbolType(items[0]) };

            theRule.Right = new List<Symbol>();
            for (int i = 1; i < items.Length; i++)
            {
                theRule.Right.Add(new Symbol { Value = items[i], Type = GetSymbolType(items[i]) });
            }

            return theRule;
        }

        private static SymbolType GetSymbolType(string item)
        {
            if (item == "[Null]")
            {
                return SymbolType.Epsilon;
            }

            if ('A' <= item[0] && item[0] <= 'Z')
            {
                return SymbolType.NonTerminal;
            }

            if ('a' <= item[0] && item[0] <= 'z')
            {
                return SymbolType.Terminal;
            }

            return SymbolType.None;
        }
    }
}
