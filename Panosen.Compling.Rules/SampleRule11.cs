using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public class SampleRule11 : SampleRule
    {
        public override string GetSamples()
        {
            return "select name, age from book;";
        }

        public override List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            rules.Add(ToRule("SELECT_STATEMENT", "SELECT_PART", "FROM_PART", "END"));

            rules.Add(ToRule("SELECT_PART", "select", "COLUMNS"));

            rules.Add(ToRule("COLUMNS", "COLUNS", "COLUMNS_RIGHT"));
            rules.Add(ToRule("COLUMNS_RIGHT", ",", "COLUNS", "COLUMNS_RIGHT"));

            rules.Add(ToRule("COLUMNS", "name"));
            rules.Add(ToRule("COLUMN", "age"));

            rules.Add(ToRule("FROM_PART", "from", "TABLE_NAME"));

            rules.Add(ToRule("END", ";"));
            rules.Add(ToRule("END", "[Null]"));

            //字母
            for (int index = 'a'; index <= 'z'; index++)
            {
                rules.Add(ToRule("LETTER", ((char)index).ToString()));
            }

            return rules;
        }
    }
}
