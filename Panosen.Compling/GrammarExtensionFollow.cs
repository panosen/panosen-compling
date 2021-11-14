using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    public static class GrammarExtensionFollow
    {
        /// <summary>
        /// 求非终结符的Follow集
        /// </summary>
        /// <param name="symbol">非终结符</param>
        /// <returns>symbol的Follow集</returns>
        public static HashSet<Symbol> GetFollowSet(this Grammar grammar, Symbol symbol)
        {
            var followSetMap = BuildFollowSetMap(grammar);

            if (followSetMap.ContainsKey(symbol))
            {
                return followSetMap[symbol];
            }

            return new HashSet<Symbol>();
        }

        public static Dictionary<Symbol, HashSet<Symbol>> BuildFollowSetMap(this Grammar grammar)
        {
            Dictionary<Symbol, HashSet<Symbol>> followSetMap = new Dictionary<Symbol, HashSet<Symbol>>();

            Dictionary<Symbol, HashSet<Symbol>> firstSetMap = grammar.BuildFirstSetMap();

            // 对起始终结符 S ，将 $ 加入 FOLLOW(S)
            followSetMap.Add(grammar.Symbols.First(), new HashSet<Symbol> { Symbols.Dollar });

            while (true)
            {
                int followSetSize = followSetMap.Values.Sum(v => v.Count);

                ScanProductionRules(followSetMap, grammar.Rules, firstSetMap);

                if (followSetSize == followSetMap.Values.Sum(v => v.Count))
                {
                    break;
                }
            }

            return followSetMap;
        }

        private static void ScanProductionRules(Dictionary<Symbol, HashSet<Symbol>> followSetMap, List<ProductionRule> productionRules, Dictionary<Symbol, HashSet<Symbol>> firstSetMap)
        {
            foreach (var productionRule in productionRules)
            {
                ScanProductionRules(followSetMap, productionRules, productionRule.Left, firstSetMap);
            }
        }

        private static void ScanProductionRules(Dictionary<Symbol, HashSet<Symbol>> followSetMap, List<ProductionRule> productionRules, Symbol symbol, Dictionary<Symbol, HashSet<Symbol>> firstSetMap)
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
                            AddToFollowSet(followSetMap, symbol, symbolNext);
                        }
                        else if (symbolNext.Type == SymbolType.NonTerminal)
                        {
                            var symbolNextFirstSet = GetSet(firstSetMap, symbolNext);
                            AddToFollowSet(followSetMap, symbol, symbolNextFirstSet.Where(v => v.Type != SymbolType.Epsilon));
                            if (symbolNextFirstSet.Any(v => v.Type == SymbolType.Epsilon))
                            {
                                if (index + 2 < productionRule.Right.Count)
                                {
                                    var symbolNextNext = productionRule.Right[index + 2];
                                    if (symbolNextNext.Type == SymbolType.Terminal)
                                    {
                                        AddToFollowSet(followSetMap, symbol, symbolNextNext);
                                    }
                                    else
                                    {
                                        AddToFollowSet(followSetMap, symbol, GetSet(firstSetMap, symbolNextNext));
                                    }
                                }
                                else
                                {
                                    AddToFollowSet(followSetMap, symbol, GetSet(followSetMap, productionRule.Left));
                                }
                            }
                        }
                    }
                    else
                    {
                        AddToFollowSet(followSetMap, symbol, GetSet(followSetMap, productionRule.Left));
                    }
                }
            }
        }

        private static HashSet<Symbol> GetSet(Dictionary<Symbol, HashSet<Symbol>> setMap, Symbol symbol)
        {
            if (setMap.ContainsKey(symbol))
            {
                return setMap[symbol];
            }

            return new HashSet<Symbol>();
        }

        private static void AddToFollowSet(Dictionary<Symbol, HashSet<Symbol>> followSetMap, Symbol symbol, Symbol item)
        {
            if (!followSetMap.ContainsKey(symbol))
            {
                followSetMap.Add(symbol, new HashSet<Symbol>());
            }

            followSetMap[symbol].Add(item);
        }

        private static void AddToFollowSet(Dictionary<Symbol, HashSet<Symbol>> followSetMap, Symbol symbol, IEnumerable<Symbol> items)
        {
            if (items == null)
            {
                return;
            }

            if (!followSetMap.ContainsKey(symbol))
            {
                followSetMap.Add(symbol, new HashSet<Symbol>());
            }

            foreach (var item in items)
            {
                followSetMap[symbol].Add(item);
            }
        }
    }
}
