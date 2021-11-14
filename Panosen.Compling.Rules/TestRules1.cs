using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public static class TestRules1
    {
        public static List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("S", "A"));
            rules.Add(ToRule("A", "A", "+", "B"));
            rules.Add(ToRule("A", "B"));
            rules.Add(ToRule("B", "B", "*", "C"));
            rules.Add(ToRule("B", "C"));
            rules.Add(ToRule("C", "(", "A", ")"));
            rules.Add(ToRule("C", "i"));

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
            switch (item)
            {
                case "A":
                case "S":
                case "B":
                case "C":
                    return SymbolType.NonTerminal;
                case "(":
                case ")":
                case "i":
                case "+":
                case "*":
                    return SymbolType.Terminal;
                default:
                    throw new NotSupportedException($"unknown symbol '{item}'.");
            }
        }
    }
}
