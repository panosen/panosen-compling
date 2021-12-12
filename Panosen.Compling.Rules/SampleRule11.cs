using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public class SampleRule11 : SampleRule
    {
        public override string GetSamples()
        {
            return "p { color : red ; }";
        }

        public override List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("STYLES", "STYLE"));
            rules.Add(ToRule("STYLE", "ID", "{", "LINE", "}"));
            rules.Add(ToRule("LINE", "KEY", ":", "VALUE", ";"));
            rules.Add(ToRule("ID", "p"));
            rules.Add(ToRule("ID", "span"));
            rules.Add(ToRule("ID", "body"));
            rules.Add(ToRule("KEY", "color"));
            rules.Add(ToRule("KEY", "width"));
            rules.Add(ToRule("KEY", "size"));
            rules.Add(ToRule("VALUE", "15px"));
            rules.Add(ToRule("VALUE", "red"));

            return rules;
        }
    }
}
