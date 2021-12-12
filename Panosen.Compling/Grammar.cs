using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace Panosen.Compling
{
    /// <summary>
    /// 文法
    /// </summary>
    public class Grammar
    {
        /// <summary>
        /// 文法
        /// </summary>
        public List<ProductionRule> Rules { get; set; }

        /// <summary>
        /// 文法符号 = 非终结符 U 终结符
        /// </summary>
        public List<Symbol> Symbols
        {
            get
            {
                List<Symbol> temp = new List<Symbol>();
                temp.AddRange(this.Rules.Select(v => v.Left));
                temp.AddRange(this.Rules.SelectMany(v => v.Right));
                temp.Add(Compling.Symbols.Dollar);
                return temp.Distinct().ToList();
            }
        }

        /// <summary>
        /// 非终结符
        /// </summary>
        public List<Symbol> NonTerminals
        {
            get
            {
                List<Symbol> temp = new List<Symbol>();
                temp.AddRange(this.Rules.Select(v => v.Left));
                temp.AddRange(this.Rules.SelectMany(v => v.Right).Where(v => v.Type == SymbolType.NonTerminal));
                return temp.Distinct().ToList();
            }
        }

        /// <summary>
        /// 终结符
        /// </summary>
        public List<Symbol> Terminals
        {
            get
            {
                return this.Rules.SelectMany(v => v.Right).Where(v => v.Type == SymbolType.Terminal).Distinct().ToList();
            }
        }
    }
}
