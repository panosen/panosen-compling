using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public static class TestRules9
    {
        /*
        A→BCc | gDB
        B→bCDE | ε
        C→DaB | ca
        D→dD | ε
        E→gAf | c
    */

        public static List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("A", "B", "C", "c"));
            rules.Add(ToRule("A", "g", "D", "B"));
            rules.Add(ToRule("B", "b", "C", "D", "E"));
            rules.Add(ToRule("B", "[Null]"));
            rules.Add(ToRule("C", "D", "a", "B"));
            rules.Add(ToRule("C", "c", "a"));
            rules.Add(ToRule("D", "d", "D"));
            rules.Add(ToRule("D", "[Null]"));
            rules.Add(ToRule("E", "g", "A", "f"));
            rules.Add(ToRule("E", "c"));

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
