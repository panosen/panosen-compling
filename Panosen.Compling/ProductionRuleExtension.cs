using System.Collections.Generic;

namespace Panosen.Compling
{
    public static class ProductionRuleExtension
    {
        public static ProductionRule AddLeft(this ProductionRule productionRule, string value)
        {
            Symbol symbol = new Symbol();
            symbol.Type = SymbolType.NonTerminal;
            symbol.Value = value;

            productionRule.Left = symbol;

            return productionRule;
        }

        public static ProductionRule AddLeft(this ProductionRule productionRule, Symbol symbol)
        {
            productionRule.Left = symbol;

            return productionRule;
        }

        public static ProductionRule AddRight(this ProductionRule productionRule, Symbol symbol)
        {
            if (productionRule.Right == null)
            {
                productionRule.Right = new List<Symbol>();
            }

            productionRule.Right.Add(symbol);

            return productionRule;
        }

        public static ProductionRule AddRight(this ProductionRule productionRule, SymbolType symbolType, string value)
        {
            Symbol symbol = new Symbol();
            symbol.Type = symbolType;
            symbol.Value = value;

            return AddRight(productionRule, symbol);
        }

        public static ProductionRule AddRightOfTerminal(this ProductionRule productionRule, string value)
        {
            Symbol symbol = new Symbol();
            symbol.Type = SymbolType.Terminal;
            symbol.Value = value;

            return AddRight(productionRule, symbol);
        }

        public static ProductionRule AddRightOfNonTerminal(this ProductionRule productionRule, string value)
        {
            Symbol symbol = new Symbol();
            symbol.Type = SymbolType.NonTerminal;
            symbol.Value = value;

            return AddRight(productionRule, symbol);
        }

        public static bool EqualsTo(this ProductionRule rule, ProductionRule other)
        {
            if (!rule.Left.Equals(other.Left))
            {
                return false;
            }
            if (rule.Right.Count != other.Right.Count)
            {
                return false;
            }
            for (int i = 0; i < rule.Right.Count; i++)
            {
                if (!rule.Right[i].Equals(other.Right[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
