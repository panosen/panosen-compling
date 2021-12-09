using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public class SampleRule9 : SampleRule
    {
        /*
        A→BCc | gDB
        B→bCDE | ε
        C→DaB | ca
        D→dD | ε
        E→gAf | c
    */

        public override string GetSamples()
        {
            throw new NotImplementedException();
        }

        public override List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("A", "B", "C", "c"));
            rules.Add(ToRule("A", "g", "D", "B"));
            rules.Add(ToRule("B", "b", "C", "D", "E"));
            rules.Add(ToRule("B", "[Null]"));
            rules.Add(ToRule("C", "D", "a", "B"));
            rules.Add(ToRule("C", "c", "a"));
            rules.Add(ToRule("D", "d", "D"));
            rules.Add(ToRule("D", "[Null]"));
            rules.Add(ToRule("E", "g", "A", "f"));
            rules.Add(ToRule("E", "c"));

            return rules;
        }
    }
}
