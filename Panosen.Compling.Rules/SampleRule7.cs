using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public class SampleRule7 : SampleRule
    {
        /*
    E → TM
    M → +TM
    M → ε
    T → FN
    N → *FN
    N → ε
    F → (E)
    F → id
    */
        public override string GetSamples()
        {
            throw new NotImplementedException();
        }

        public override List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("E", "T", "M"));
            rules.Add(ToRule("M", "+", "T", "M"));
            rules.Add(ToRule("M", "[Null]"));
            rules.Add(ToRule("T", "F", "N"));
            rules.Add(ToRule("N", "*", "F", "N"));
            rules.Add(ToRule("N", "[Null]"));
            rules.Add(ToRule("F", "(", "E", ")"));
            rules.Add(ToRule("F", "id"));

            return rules;
        }
    }
}
