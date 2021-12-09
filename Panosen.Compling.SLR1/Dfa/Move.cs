using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    public class Move
    {
        public TheState From { get; set; }
        public TheState To { get; set; }
        public Symbol By { get; set; }
    }
}
