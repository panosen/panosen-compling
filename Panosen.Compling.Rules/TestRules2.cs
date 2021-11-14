using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public static class TestRules2
    {
        public static List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("S", "E"));
            rules.Add(ToRule("E", "T", "A"));
            rules.Add(ToRule("A", "+", "T", "A"));
            rules.Add(ToRule("A", "[Null]"));
            rules.Add(ToRule("T", "F", "B"));
            rules.Add(ToRule("B", "*", "F", "B"));
            rules.Add(ToRule("B", "[Null]"));
            rules.Add(ToRule("F", "i"));
            rules.Add(ToRule("F", "l", "E", "r"));

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
                case "E":
                case "A":
                case "S":
                case "T":
                case "B":
                case "F":
                    return SymbolType.NonTerminal;
                case "l":
                case "i":
                case "+":
                case "*":
                case "r":
                    return SymbolType.Terminal;
                case "[Null]":
                    return SymbolType.Epsilon;
                default:
                    throw new NotSupportedException($"unknown symbol '{item}'.");
            }
        }
    }
}
