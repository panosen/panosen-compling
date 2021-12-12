using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Compling
{
    public class WhitespaceTokenizer : ITokenizer
    {
        public TokenCollection Analyze(string text)
        {
            TokenCollection tokenCollection = new TokenCollection();

            StringBuilder stringBuilder = new StringBuilder();

            var sourceReader = new SourceReader(text);
            while (sourceReader.ViewOne() != null)
            {
                var value = sourceReader.Read().Value.ToString();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    stringBuilder.Append(value);
                    continue;
                }

                if (stringBuilder.Length > 0)
                {
                    tokenCollection.AddToken(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
            }

            if (stringBuilder.Length > 0)
            {
                tokenCollection.AddToken(stringBuilder.ToString());
            }

            return tokenCollection;
        }
    }
}
