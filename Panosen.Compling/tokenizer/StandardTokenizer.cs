using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    /// <summary>
    /// 词法分析
    /// </summary>
    public class StandardTokenizer : ITokenizer
    {
        /// <summary>
        /// 分析
        /// </summary>
        public TokenCollection Analyze(string text)
        {
            TokenCollection tokens = new TokenCollection();

            var reader = new SourceReader(text);
            while (reader.ViewOne() != null)
            {
                var row = reader.Row;
                var col = reader.Col;
                var value = reader.Read().Value.ToString();

                tokens.AddToken(value: value, row: row, col: col);
            }

            return tokens;
        }
    }
}
