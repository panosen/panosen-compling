using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public class SampleRule2: SampleRule
    {
        public override string GetSamples()
        {
            return "i + i";
        }

        public override List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("S", "E"));
            rules.Add(ToRule("E", "T", "A"));
            rules.Add(ToRule("A", "+", "T", "A"));
            rules.Add(ToRule("A", "[Null]"));
            rules.Add(ToRule("T", "F", "B"));
            rules.Add(ToRule("B", "*", "F", "B"));
            rules.Add(ToRule("B", "[Null]"));
            rules.Add(ToRule("F", "i"));
            rules.Add(ToRule("F", "l", "E", "r"));

            return rules;
        }
    }
}
