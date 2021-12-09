using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling.Rules
{
    public class SampleRule10 : SampleRule
    {
        public override string GetSamples()
        {
            throw new NotImplementedException();
        }

        public override List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("START", "LEFT", "RIGHT"));
            rules.Add(ToRule("RIGHT", "+", "LEFT", "RIGHT"));
            rules.Add(ToRule("RIGHT", "[Null]"));
            rules.Add(ToRule("LEFT", "id"));

            return rules;
        }
    }
}
