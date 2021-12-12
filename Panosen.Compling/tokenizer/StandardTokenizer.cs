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
        public TokenCollection Analyze(string text)
        {
            TokenCollection tokens = new TokenCollection();

            var sourceReader = new SourceReader(text);
            while (sourceReader.ViewOne() != null)
            {
                var token = tokens.AddToken();

                token.Value = sourceReader.Read().Value.ToString();
            }

            return tokens;
        }
    }
}
