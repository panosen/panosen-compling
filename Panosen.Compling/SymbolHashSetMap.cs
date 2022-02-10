using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    /// <summary>
    /// SymbolHashSetMap
    /// </summary>
    public class SymbolHashSetMap
    {
        /// <summary>
        /// Map
        /// </summary>
        public Dictionary<Symbol, HashSet<Symbol>> Map { get; } = new Dictionary<Symbol, HashSet<Symbol>>();
    }

    /// <summary>
    /// SymbolHashSetMapExtension
    /// </summary>
    public static class SymbolHashSetMapExtension
    {
        /// <summary>
        /// 总数
        /// </summary>
        public static int TotalCount(this SymbolHashSetMap symbolHashSetMap)
        {
            if (symbolHashSetMap == null)
            {
                return 0;
            }

            if (symbolHashSetMap.Map == null || symbolHashSetMap.Map.Count == 0)
            {
                return 0;
            }

            return symbolHashSetMap.Map.Values.Sum(v => v.Count);
        }

        /// <summary>
        /// ContainsKey
        /// </summary>
        public static bool ContainsKey(this SymbolHashSetMap symbolHashSetMap, Symbol symbol)
        {
            return symbolHashSetMap.Map.ContainsKey(symbol);
        }

        /// <summary>
        /// this
        /// </summary>
        public static HashSet<Symbol> GetHashSet(this SymbolHashSetMap symbolHashSetMap, Symbol symbol)
        {
            if (symbolHashSetMap.Map.ContainsKey(symbol))
            {
                return symbolHashSetMap.Map[symbol];
            }

            return new HashSet<Symbol>();
        }

        /// <summary>
        /// Add
        /// </summary>
        public static void Add(this SymbolHashSetMap followSetMap, Symbol symbol, Symbol item)
        {
            if (!followSetMap.Map.ContainsKey(symbol))
            {
                followSetMap.Map.Add(symbol, new HashSet<Symbol>());
            }

            followSetMap.Map[symbol].Add(item);
        }

        /// <summary>
        /// AddRange
        /// </summary>
        public static void AddRange(this SymbolHashSetMap followSetMap, Symbol symbol, IEnumerable<Symbol> items)
        {
            if (items == null || items.Count() == 0)
            {
                return;
            }

            if (!followSetMap.Map.ContainsKey(symbol))
            {
                followSetMap.Map.Add(symbol, new HashSet<Symbol>());
            }

            foreach (var item in items)
            {
                followSetMap.Map[symbol].Add(item);
            }
        }
    }
}
