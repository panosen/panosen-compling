using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    /// <summary>
    /// ProductionRule
    /// </summary>
    public class ProductionRule
    {
        /// <summary>
        /// Left
        /// </summary>
        public Symbol Left { get; set; }

        /// <summary>
        /// Right
        /// </summary>
        public List<Symbol> Right { get; set; }
    }
}
