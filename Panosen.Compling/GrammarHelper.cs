using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    /// <summary>
    /// GrammarExtension
    /// </summary>
    public static class GrammarHelper
    {
        /// <summary>
        /// 文法符号 = 非终结符 U 终结符
        /// </summary>
        public static List<Symbol> GetSymbols(Grammar grammar)
        {
            List<Symbol> temp = new List<Symbol>();
            temp.AddRange(grammar.Rules.Select(v => v.Left));
            temp.AddRange(grammar.Rules.SelectMany(v => v.Right));
            temp.Add(Compling.Symbols.Dollar);
            return temp.Distinct().ToList();
        }
    }
}
