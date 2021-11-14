using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public static class TestRules6
    {
        public static List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("Style", "P"));
            rules.Add(ToRule("P", "ID", "{", "S", "}"));
            rules.Add(ToRule("S", "S", ";", "Line"));
            rules.Add(ToRule("S", "Line"));
            rules.Add(ToRule("Line", "Key", ":", "Value"));

            rules.Add(ToRule("ID", "p"));
            rules.Add(ToRule("ID", "span"));
            rules.Add(ToRule("ID", "body"));

            rules.Add(ToRule("Key", "color"));
            rules.Add(ToRule("Key", "width"));

            rules.Add(ToRule("Value", "red"));
            rules.Add(ToRule("Value", "15px"));

            return rules;
        }

        public static string GetSample()
        {
            return "body { color : red ; width : 15px }";
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
                case "Style":
                case "Styles":
                case "Value":
                case "ID":
                case "Key":
                case "Line":
                case "Lines":
                case "Plus":
                case "A":
                case "B":
                case "C":
                case "S":
                case "P":
                case "X":
                    return SymbolType.NonTerminal;

                case ":":
                case ";":
                case "{":
                case "}":

                case "body":
                case "p":
                case "span":

                case "color":
                case "width":

                case "red":
                case "15px":

                    return SymbolType.Terminal;
                case "[Null]":
                    return SymbolType.Epsilon;
                default:
                    throw new NotSupportedException($"unknown symbol '{item}'.");
            }
        }
    }
}
