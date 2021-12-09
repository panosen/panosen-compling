using Panosen.Compling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    public static class TheSymbolExtension
    {
        public static string ToString1(this Symbol symbol)
        {
            if (symbol.Value == "[Dollar]")
            {
                return "$";
            }
            if (symbol.Type == SymbolType.Epsilon && symbol.Value == "[Null]")
            {
                return "ε";
            }
            if (symbol.Value == "[Point]")
            {
                return "•";
            }
            return symbol.Value;
        }
    }
}
