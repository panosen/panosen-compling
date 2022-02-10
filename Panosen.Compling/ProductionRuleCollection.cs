using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    /// <summary>
    /// ProductionRuleCollection
    /// </summary>
    public class ProductionRuleCollection
    {
        /// <summary>
        /// 文法
        /// </summary>
        public List<ProductionRule> ProductionRules { get; set; }

        /// <summary>
        /// ProductionRuleCollection
        /// </summary>
        public ProductionRuleCollection(List<ProductionRule> productionRules)
        {
            ProductionRules = productionRules;

            this.RefreshSymbolType();
        }
    }

    /// <summary>
    /// ProductionRuleCollectionExtension
    /// </summary>
    public static class ProductionRuleCollectionExtension
    {
        /// <summary>
        /// AddProductionRule
        /// </summary>
        public static ProductionRule AddProductionRule(this ProductionRuleCollection productionRuleCollection)
        {
            if (productionRuleCollection.ProductionRules == null)
            {
                productionRuleCollection.ProductionRules = new List<ProductionRule>();
            }

            var productionRule = new ProductionRule();
            productionRuleCollection.ProductionRules.Add(productionRule);

            return productionRule;
        }

        /// <summary>
        /// RefreshSymbolType
        /// </summary>
        public static void RefreshSymbolType(this ProductionRuleCollection productionRuleCollection)
        {
            var nonTerminalSet = productionRuleCollection.ProductionRules.Select(v => v.Left.Value).Distinct().ToList();
            foreach (var productionRule in productionRuleCollection.ProductionRules)
            {
                productionRule.Left.Type = GetSymbolType(nonTerminalSet, productionRule.Left.Value);

                foreach (var symbol in productionRule.Right)
                {
                    symbol.Type = GetSymbolType(nonTerminalSet, symbol.Value);
                }
            }
        }

        private static SymbolType GetSymbolType(List<string> nonTerminalSet, string symbol)
        {
            if (symbol.Equals("[Null]"))
            {
                return SymbolType.Epsilon;
            }

            if (nonTerminalSet.Contains(symbol))
            {
                return SymbolType.NonTerminal;
            }

            return SymbolType.Terminal;
        }
    }
}
