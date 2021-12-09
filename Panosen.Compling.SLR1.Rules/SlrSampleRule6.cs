using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public class SlrSampleRule6 : SampleRule
    {
        public override string GetSamples()
        {
            return "body { color : red ; width : 15px }";
        }

        public override List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("Style", "P"));
            rules.Add(ToRule("P", "ID", "{", "S", "}"));
            rules.Add(ToRule("S", "S", ";", "Line"));
            rules.Add(ToRule("S", "Line"));
            rules.Add(ToRule("Line", "Key", ":", "Value"));

            rules.Add(ToRule("ID", "p"));
            rules.Add(ToRule("ID", "span"));
            rules.Add(ToRule("ID", "body"));

            rules.Add(ToRule("Key", "color"));
            rules.Add(ToRule("Key", "width"));

            rules.Add(ToRule("Value", "red"));
            rules.Add(ToRule("Value", "15px"));

            return rules;
        }
    }
}
