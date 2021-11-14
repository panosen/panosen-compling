using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    public static class TheTableCellExtension
    {

        public static string ToString2(this TheTableCell theTableCell)
        {
            return $"{theTableCell.Type} {theTableCell.Value}";
        }
    }
}
