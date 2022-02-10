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
            theRule.Left = new Symbol { Value = items[0]};

            theRule.Right = new List<Symbol>();
            for (int i = 1; i < items.Length; i++)
            {
                theRule.Right.Add(new Symbol { Value = items[i]});
            }

            return theRule;
        }
    }
}
