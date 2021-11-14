using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    public enum SymbolType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        None,

        /// <summary>
        /// 非终结符
        /// </summary>
        NonTerminal,

        /// <summary>
        /// 终结符
        /// </summary>
        Terminal,

        /// <summary>
        /// 空串
        /// </summary>
        Epsilon
    }

    public struct Symbol
    {

        public SymbolType Type { get; set; }

        public string Value { get; set; }
    }
    public class Symbols
    {
        /// <summary>
        /// Point
        /// </summary>
        public static readonly Symbol Point = new Symbol { Type = SymbolType.Terminal, Value = "[Point]" };

        /// <summary>
        /// Epsilon
        /// </summary>
        public static readonly Symbol Epsilon = new Symbol { Type = SymbolType.Epsilon, Value = "[Null]" };

        /// <summary>
        /// Dollar
        /// </summary>
        public static readonly Symbol Dollar = new Symbol { Type = SymbolType.Terminal, Value = "[Dollar]" };
    }
}
