using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public static class TestRules4
    {
        public static List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("S'", "S"));
            rules.Add(ToRule("S", "a", "E"));
            rules.Add(ToRule("E", "a", "E"));
            rules.Add(ToRule("E", "[Null]"));

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
                case "S'":
                case "E":
                case "E'":
                case "T":
                case "F":
                    return SymbolType.NonTerminal;
                case "a":
                case "id":
                case "+":
                case "*":
                    return SymbolType.Terminal;
                case "[Null]":
                    return SymbolType.Epsilon;
                default:
                    throw new NotSupportedException($"unknown symbol '{item}'.");
            }
        }
    }
}
