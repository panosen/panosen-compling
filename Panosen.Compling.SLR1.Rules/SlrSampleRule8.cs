using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public class SlrSampleRule8 : SampleRule
    {
        /*
S→AB
S→bC
A→ε
A→b
B→ε
B→aD
C→AD
C→b
D→aS
D→c
    */

        public override string GetSamples()
        {
            throw new NotImplementedException();
        }

        public override List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("S", "A", "B"));
            rules.Add(ToRule("S", "b", "C"));
            rules.Add(ToRule("A", "[Null]"));
            rules.Add(ToRule("A", "b"));
            rules.Add(ToRule("B", "[Null]"));
            rules.Add(ToRule("B", "a", "D"));
            rules.Add(ToRule("C", "A", "D"));
            rules.Add(ToRule("C", "b"));
            rules.Add(ToRule("D", "a", "S"));
            rules.Add(ToRule("D", "c"));

            return rules;
        }
    }
}
