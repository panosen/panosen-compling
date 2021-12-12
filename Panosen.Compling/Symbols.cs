namespace Panosen.Compling
{
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
