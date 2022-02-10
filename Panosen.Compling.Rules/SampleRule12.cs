using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public class SampleRule12 : SampleRule
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

            rules.Add(ToRule("IDENTIFIER", "LETTER", "IDENTIFIER_RIGHT"));
            rules.Add(ToRule("IDENTIFIER_RIGHT", "LETTER", "IDENTIFIER_RIGHT"));
            rules.Add(ToRule("IDENTIFIER_RIGHT", "[Null]"));

            rules.Add(ToRule("LETTER_OR_NUMBER", "LETTER"));
            rules.Add(ToRule("LETTER_OR_NUMBER", "NUMBER"));

            //字母
            for (int index = 'a'; index <= 'z'; index++)
            {
                rules.Add(ToRule("LETTER", ((char)index).ToString()));
            }
            for (int index = 'A'; index <= 'Z'; index++)
            {
                rules.Add(ToRule("LETTER", ((char)index).ToString()));
            }
            //数字
            for (int index = '0'; index <= '9'; index++)
            {
                rules.Add(ToRule("NUMBER", ((char)index).ToString()));
            }

            return rules;
        }
    }
}
