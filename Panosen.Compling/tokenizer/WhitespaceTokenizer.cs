using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Compling
{
    /// <summary>
    /// 空格分析器
    /// </summary>
    public class WhitespaceTokenizer : ITokenizer
    {
        /// <summary>
        /// 分析
        /// </summary>
        public TokenCollection Analyze(string text)
        {
            TokenCollection tokenCollection = new TokenCollection();

            StringBuilder builder = new StringBuilder();

            var row = 0;
            var col = 0;

            var reader = new SourceReader(text);
            while (reader.ViewOne() != null)
            {
                if (builder.Length == 0)
                {
                    row = reader.Row;
                    col = reader.Col;
                }

                var value = reader.Read().Value.ToString();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    builder.Append(value);
                    continue;
                }

                if (builder.Length > 0)
                {
                    tokenCollection.AddToken(value: builder.ToString(), row: row, col: col);
                    row = 0;
                    col = 0;
                    builder.Clear();
                }
            }

            if (builder.Length > 0)
            {
                tokenCollection.AddToken(value: builder.ToString(), row: row, col: col);
            }

            return tokenCollection;
        }
    }
}
