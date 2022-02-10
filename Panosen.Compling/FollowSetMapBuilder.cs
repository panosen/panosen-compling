using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    /// <summary>
    /// GrammarExtensionFollow
    /// </summary>
    public static class FollowSetMapBuilder
    {
        /// <summary>
        /// BuildFollowSetMap
        /// </summary>
        public static SymbolHashSetMap BuildFollowSetMap(Grammar grammar)
        {
            var followSetMap = new SymbolHashSetMap();

            var firstSetMap = FirstSetMapBuilder.BuildFirstSetMap(grammar);

            // 对起始终结符 S ，将 $ 加入 FOLLOW(S)
            followSetMap.Add(GrammarHelper.GetSymbols(grammar).First(), Symbols.Dollar);

            while (true)
            {
                int followSetSize = followSetMap.TotalCount();

                ScanProductionRules(followSetMap, grammar.Rules, firstSetMap);

                if (followSetSize == followSetMap.TotalCount())
                {
                    break;
                }
            }

            return followSetMap;
        }

        private static void ScanProductionRules(SymbolHashSetMap followSetMap, List<ProductionRule> productionRules, SymbolHashSetMap firstSetMap)
        {
            foreach (var productionRule in productionRules)
            {
                ScanProductionRules(followSetMap, productionRules, productionRule.Left, firstSetMap);
            }
        }

        private static void ScanProductionRules(SymbolHashSetMap followSetMap, List<ProductionRule> productionRules, Symbol symbol, SymbolHashSetMap firstSetMap)
        {
            foreach (var productionRule in productionRules)
            {
                if (!productionRule.Right.Contains(symbol))
                {
                    continue;
                }
                for (int index = 0; index < productionRule.Right.Count; index++)
                {
                    if (!productionRule.Right[index].Equals(symbol))
                    {
                        continue;
                    }
                    if (index + 1 < productionRule.Right.Count)
                    {
                        var symbolNext = productionRule.Right[index + 1];
                        if (symbolNext.Type == SymbolType.Terminal)
                        {
                            followSetMap.Add(symbol, symbolNext);
                        }
                        else if (symbolNext.Type == SymbolType.NonTerminal)
                        {
                            var symbolNextFirstSet = firstSetMap.GetHashSet(symbolNext);
                            followSetMap.AddRange(symbol, symbolNextFirstSet.Where(v => v.Type != SymbolType.Epsilon));
                            if (symbolNextFirstSet.Any(v => v.Type == SymbolType.Epsilon))
                            {
                                if (index + 2 < productionRule.Right.Count)
                                {
                                    var symbolNextNext = productionRule.Right[index + 2];
                                    if (symbolNextNext.Type == SymbolType.Terminal)
                                    {
                                        followSetMap.Add(symbol, symbolNextNext);
                                    }
                                    else
                                    {
                                        followSetMap.AddRange(symbol, firstSetMap.GetHashSet(symbolNextNext));
                                    }
                                }
                                else
                                {
                                    followSetMap.AddRange(symbol, followSetMap.GetHashSet(productionRule.Left));
                                }
                            }
                        }
                    }
                    else
                    {
                        followSetMap.AddRange(symbol, followSetMap.GetHashSet(productionRule.Left));
                    }
                }
            }
        }
    }
}
