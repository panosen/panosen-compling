using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    public static class GrammarExtensionFirst
    {
        /// <summary>
        /// 求非终结符的First集
        /// </summary>
        /// <param name="symbol">非终结符</param>
        /// <returns>返回First集的List</returns>
        public static HashSet<Symbol> GetFirstSet(this Grammar grammar, Symbol symbol)
        {
            var firstSetMap = BuildFirstSetMap(grammar);
            if (firstSetMap.ContainsKey(symbol))
            {
                return firstSetMap[symbol];
            }

            return new HashSet<Symbol>();
        }

        public static Dictionary<Symbol, HashSet<Symbol>> BuildFirstSetMap(this Grammar grammar)
        {
            Dictionary<Symbol, HashSet<Symbol>> firstSetMap = new Dictionary<Symbol, HashSet<Symbol>>();

            while (true)
            {
                int firstSetSize = firstSetMap.Values.Sum(v => v.Count);

                ScanProductionRules(firstSetMap, grammar.Rules);

                if (firstSetSize == firstSetMap.Values.Sum(v => v.Count))
                {
                    break;
                }
            }

            return firstSetMap;
        }

        private static void ScanProductionRules(Dictionary<Symbol, HashSet<Symbol>> firstSetMap, List<ProductionRule> productionRules)
        {
            foreach (var productionRule in productionRules)
            {
                ScanProductionRules(firstSetMap, productionRules, productionRule.Left);
            }
        }

        private static void ScanProductionRules(Dictionary<Symbol, HashSet<Symbol>> firstSetMap, List<ProductionRule> productionRules, Symbol symbol)
        {
            foreach (var productionRule in productionRules)
            {
                if (!productionRule.Left.Equals(symbol))
                {
                    continue;
                }

                for (int index = 0; index < productionRule.Right.Count; index++)
                {
                    var currentSymbol = productionRule.Right[index];
                    if (currentSymbol.Type == SymbolType.Epsilon)
                    {
                        AddToFirstSet(firstSetMap, symbol, currentSymbol);
                        break;
                    }
                    if (currentSymbol.Type == SymbolType.Terminal)
                    {
                        AddToFirstSet(firstSetMap, symbol, currentSymbol);
                        break;
                    }
                    if (currentSymbol.Type == SymbolType.NonTerminal)
                    {
                        var currentSymbolFirstSet = GetFirstSet(firstSetMap, currentSymbol);
                        if (currentSymbolFirstSet.Any(v => v.Type == SymbolType.Epsilon))
                        {
                            if (index == productionRule.Right.Count - 1)
                            {
                                AddToFirstSet(firstSetMap, symbol, currentSymbolFirstSet);
                            }
                            else
                            {
                                AddToFirstSet(firstSetMap, symbol, currentSymbolFirstSet.Where(s => s.Type != SymbolType.Epsilon));
                            }
                        }
                        else
                        {
                            AddToFirstSet(firstSetMap, symbol, GetFirstSet(firstSetMap, currentSymbol));
                            break;
                        }
                    }
                }
            }
        }

        private static HashSet<Symbol> GetFirstSet(Dictionary<Symbol, HashSet<Symbol>> firstSetMap, Symbol symbol)
        {
            if (firstSetMap.ContainsKey(symbol))
            {
                return firstSetMap[symbol];
            }

            return new HashSet<Symbol>();
        }

        private static void AddToFirstSet(Dictionary<Symbol, HashSet<Symbol>> firstSetMap, Symbol symbol, Symbol item)
        {
            if (!firstSetMap.ContainsKey(symbol))
            {
                firstSetMap.Add(symbol, new HashSet<Symbol>());
            }

            firstSetMap[symbol].Add(item);
        }

        private static void AddToFirstSet(Dictionary<Symbol, HashSet<Symbol>> firstSetMap, Symbol symbol, IEnumerable<Symbol> items)
        {
            if (items == null)
            {
                return;
            }

            if (!firstSetMap.ContainsKey(symbol))
            {
                firstSetMap.Add(symbol, new HashSet<Symbol>());
            }

            foreach (var item in items)
            {
                firstSetMap[symbol].Add(item);
            }
        }
    }
}
