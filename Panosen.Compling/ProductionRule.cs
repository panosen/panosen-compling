using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    public class ProductionRule
    {
        public Symbol Left { get; set; }

        public List<Symbol> Right { get; set; }
    }
}
