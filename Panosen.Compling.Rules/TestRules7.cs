using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public static class TestRules7
    {
        /*
    E → TM
    M → +TM
    M → ε
    T → FN
    N → *FN
    N → ε
    F → (E)
    F → id
    */
        public static List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("E", "T", "M"));
            rules.Add(ToRule("M", "+", "T", "M"));
            rules.Add(ToRule("M", "[Null]"));
            rules.Add(ToRule("T", "F", "N"));
            rules.Add(ToRule("N", "*", "F", "N"));
            rules.Add(ToRule("N", "[Null]"));
            rules.Add(ToRule("F", "(", "E", ")"));
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
                case "E":
                case "M":
                case "T":
                case "N":
                case "F":
                    return SymbolType.NonTerminal;
                case "(":
                case ")":
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
