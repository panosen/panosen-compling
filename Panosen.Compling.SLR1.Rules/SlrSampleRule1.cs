using Panosen.Compling.Rules;
using System;
using System.Collections.Generic;

namespace Panosen.Compling.SLR1.Rules
{
    public class SlrSampleRule1 : SampleRule
    {
        public override string GetSamples()
        {
            return "i * ( ( ( i * i * i ) + i * ( i * i ) ) * i ) * i"; ;
        }

        public override List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("START", "ONE"));
            rules.Add(ToRule("ONE", "ONE", "+", "TWO"));
            rules.Add(ToRule("ONE", "TWO"));
            rules.Add(ToRule("TWO", "TWO", "*", "THREE"));
            rules.Add(ToRule("TWO", "THREE"));
            rules.Add(ToRule("THREE", "(", "ONE", ")"));
            rules.Add(ToRule("THREE", "i"));

            return rules;
        }
    }
}
