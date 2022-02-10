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
            rules.Add(ToRule("STYLE", "ID", "WHITESPACES", "{", "WHITESPACES", "LINE", "WHITESPACES", "}"));
            rules.Add(ToRule("LINE", "KEY", "WHITESPACES", ":", "WHITESPACES", "VALUE", ";"));

            rules.Add(ToRule("ID", "LETTER", "ID_RIGHT"));
            rules.Add(ToRule("ID_RIGHT", "LETTER", "ID_RIGHT"));
            rules.Add(ToRule("ID_RIGHT", "[Null]"));

            rules.Add(ToRule("KEY", "LETTER", "KEY_RIGHT"));
            rules.Add(ToRule("KEY_RIGHT", "LETTER", "KEY_RIGHT"));
            rules.Add(ToRule("KEY_RIGHT", "[Null]"));

            rules.Add(ToRule("VALUE", "LETTER", "VALUE_RIGHT"));
            rules.Add(ToRule("VALUE_RIGHT", "LETTER", "VALUE_RIGHT"));
            rules.Add(ToRule("VALUE_RIGHT", "[Null]"));

            rules.Add(ToRule("WHITESPACES", "[Null]"));
            rules.Add(ToRule("WHITESPACES", "WHITESPACE", "WHITESPACES_RIGHT"));
            rules.Add(ToRule("WHITESPACES_RIGHT", "WHITESPACE", "WHITESPACES_RIGHT"));
            rules.Add(ToRule("WHITESPACES_RIGHT", "[Null]"));

            rules.Add(ToRule("WHITESPACE", " "));
            rules.Add(ToRule("WHITESPACE", "\t"));

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
