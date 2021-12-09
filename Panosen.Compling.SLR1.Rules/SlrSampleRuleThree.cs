using Panosen.Compling.Rules;
using System;
using System.Collections.Generic;

namespace Panosen.Compling.SLR1.Rules
{
    public class SlrSampleRuleThree : SampleRule
    {
        public override string GetSamples()
        {
            return "id * id + id * id";
        }

        public override List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("S", "E"));
            rules.Add(ToRule("E", "E", "+", "T"));
            rules.Add(ToRule("E", "T"));
            rules.Add(ToRule("T", "T", "*", "F"));
            rules.Add(ToRule("T", "F"));
            rules.Add(ToRule("F", "id"));

            return rules;
        }
    }
}
