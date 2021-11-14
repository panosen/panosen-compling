using Panosen.Compling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling.Display
{
    public static class TheRuleExtension
    {
        public static string ToString1(this ProductionRule rule)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{rule.Left.ToString1()} → ");
            foreach (var s in rule.Right)
            {
                sb.Append($"{s.ToString1()}");
            }
            return sb.ToString();
        }
    }
}
