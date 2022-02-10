using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    /// <summary>
    /// GrammarExtensionFirst
    /// </summary>
    public static class FirstSetMapBuilder
    {
        /// <summary>
        /// BuildFirstSetMap
        /// </summary>
        public static SymbolHashSetMap BuildFirstSetMap(Grammar grammar)
        {
            SymbolHashSetMap firstSetMap = new SymbolHashSetMap();

            while (true)
            {
                int firstSetSize = firstSetMap.TotalCount();

                ScanProductionRules(firstSetMap, grammar.Rules);

                if (firstSetSize == firstSetMap.TotalCount())
                {
                    break;
                }
            }

            return firstSetMap;
        }

        private static void ScanProductionRules(SymbolHashSetMap firstSetMap, List<ProductionRule> productionRules)
        {
            foreach (var productionRule in productionRules)
            {
                ScanProductionRules(firstSetMap, productionRules, productionRule.Left);
            }
        }

        private static void ScanProductionRules(SymbolHashSetMap firstSetMap, List<ProductionRule> productionRules, Symbol symbol)
        {
            foreach (var productionRule in productionRules)
            {
                ScanProductionRule(firstSetMap, symbol, productionRule);
            }
        }

        private static void ScanProductionRule(SymbolHashSetMap firstSetMap, Symbol symbol, ProductionRule productionRule)
        {
            if (!productionRule.Left.Equals(symbol))
            {
                return;
            }

            for (int index = 0; index < productionRule.Right.Count; index++)
            {
                var currentSymbol = productionRule.Right[index];
                if (currentSymbol.Type == SymbolType.Epsilon)
                {
                    firstSetMap.Add(symbol, currentSymbol);
                    break;
                }
                if (currentSymbol.Type == SymbolType.Terminal)
                {
                    firstSetMap.Add(symbol, currentSymbol);
                    break;
                }
                if (currentSymbol.Type == SymbolType.NonTerminal)
                {
                    var currentSymbolFirstSet = firstSetMap.GetHashSet(currentSymbol);
                    if (currentSymbolFirstSet.Any(v => v.Type == SymbolType.Epsilon))
                    {
                        if (index == productionRule.Right.Count - 1)
                        {
                            firstSetMap.AddRange(symbol, currentSymbolFirstSet);
                        }
                        else
                        {
                            firstSetMap.AddRange(symbol, currentSymbolFirstSet.Where(s => s.Type != SymbolType.Epsilon));
                        }
                    }
                    else
                    {
                        firstSetMap.AddRange(symbol, firstSetMap.GetHashSet(currentSymbol));
                        break;
                    }
                }
            }
        }
    }
}
