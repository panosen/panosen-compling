using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public class SampleRule4 : SampleRule
    {
        public override string GetSamples()
        {
            return "aaaaa";
        }

        public override List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("S'", "S"));
            rules.Add(ToRule("S", "a", "E"));
            rules.Add(ToRule("E", "a", "E"));
            rules.Add(ToRule("E", "[Null]"));

            return rules;
        }
    }
}
