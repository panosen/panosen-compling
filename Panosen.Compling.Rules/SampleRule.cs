using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling.Rules
{
    public abstract class SampleRule
    {
        public abstract string GetSamples();

        public abstract List<ProductionRule> GetRules();

        protected static ProductionRule ToRule(params string[] items)
        {
            ProductionRule theRule = new ProductionRule();
            theRule.Left = new Symbol { Value = items[0], Type = GetSymbolType(items[0]) };

            theRule.Right = new List<Symbol>();
            for (int i = 1; i < items.Length; i++)
            {
                theRule.Right.Add(new Symbol { Value = items[i], Type = GetSymbolType(items[i]) });
            }

            return theRule;
        }

        protected static SymbolType GetSymbolType(string item)
        {
            if (item == "[Null]")
            {
                return SymbolType.Epsilon;
            }
            foreach (var ch in item)
            {
                if (ch < 'A' || ch > 'Z')
                {
                    return SymbolType.Terminal;
                }
            }

            return SymbolType.NonTerminal;
        }
    }
}
