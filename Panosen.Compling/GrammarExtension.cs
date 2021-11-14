using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    public static class GrammarExtension
    {
        public static ProductionRule AddProductionRule(this Grammar grammar)
        {
            if (grammar.Rules == null)
            {
                grammar.Rules = new List<ProductionRule>();
            }

            var productionRule = new ProductionRule();
            grammar.Rules.Add(productionRule);

            return productionRule;
        }
    }
}
