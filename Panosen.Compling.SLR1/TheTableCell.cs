using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    public class TheTableCell
    {
        public enum Types { NULL, SHIFT, REDUCE, GOTO, ACC};
        public Types Type { get; set; }
        public int Value { get; set; }
        public TheTableCell()
        {
            Type = Types.NULL;
            Value = -1;
        }
        public TheTableCell(Types type, int value)
        {
            Type = type;
            Value = value;
        }
    }
}
