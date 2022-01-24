using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Compling
{
    public class Token
    {
        /// <summary>
        /// 类型
        /// </summary>
        public TokenType TokenType { get; set; }

        public string Value { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }
    }
}
