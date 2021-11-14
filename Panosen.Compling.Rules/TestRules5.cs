using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public static class TestRules5
    {
        public static List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("Styles", "Style"));
            rules.Add(ToRule("Style", "ID", "{", "Line", "}"));
            rules.Add(ToRule("Line", "Key", ":", "Value", ";"));
            rules.Add(ToRule("ID", "p"));
            rules.Add(ToRule("ID", "span"));
            rules.Add(ToRule("ID", "body"));
            rules.Add(ToRule("Key", "color"));
            rules.Add(ToRule("Key", "width"));
            rules.Add(ToRule("Key", "size"));
            rules.Add(ToRule("Value", "15px"));
            rules.Add(ToRule("Value", "red"));

            return rules;
        }

        public static string GetSample()
        {
            return "p { color : red ; }";
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
                    return SymbolType.NonTerminal;
                case ":":
                case ";":
                case "{":
                case "}":
                case "15px":
                case "body":
                case "color":
                case "p":
                case "red":
                case "size":
                case "span":
                case "width":
                    return SymbolType.Terminal;
                case "[Null]":
                    return SymbolType.Epsilon;
                default:
                    throw new NotSupportedException($"unknown symbol '{item}'.");
            }
        }
    }
}
