using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public static class TestRules3
    {
        public static List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("S", "E"));
            rules.Add(ToRule("E", "E", "+", "T"));
            rules.Add(ToRule("E", "T"));
            rules.Add(ToRule("T", "T", "*", "F"));
            rules.Add(ToRule("T", "F"));
            rules.Add(ToRule("F", "id"));

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
                case "S":
                case "E":
                case "E'":
                case "T":
                case "F":
                    return SymbolType.NonTerminal;
                case "(":
                case ")":
                case "id":
                case "+":
                case "*":
                    return SymbolType.Terminal;
                default:
                    throw new NotSupportedException($"unknown symbol '{item}'.");
            }
        }
    }
}
