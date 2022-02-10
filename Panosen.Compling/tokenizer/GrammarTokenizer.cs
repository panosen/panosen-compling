using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    /// <summary>
    /// 基于语法的词法分析
    /// </summary>
    public class GrammarTokenizer : ITokenizer
    {
        private ProductionRuleCollection productionRules;

        /// <summary>
        /// 基于语法的词法分析
        /// </summary>
        public GrammarTokenizer(ProductionRuleCollection productionRules)
        {
            this.productionRules = productionRules;
        }

        /// <summary>
        /// 词法分析
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public TokenCollection Analyze(string text)
        {
            TokenCollection tokenCollection = new TokenCollection();

            var symbols = productionRules.ProductionRules.SelectMany(v => v.Right).Where(v => v.Type == SymbolType.Terminal).Distinct().ToList();

            var symbolChars = symbols.SelectMany(v => v.Value).Distinct().ToList();

            StringBuilder stringBuilder = new StringBuilder();

            var sourceReader = new SourceReader(text);
            while (sourceReader.ViewOne() != null)
            {
                var value = sourceReader.Read().Value;

                if (!symbolChars.Contains(value))
                {
                    if (stringBuilder.Length > 0)
                    {
                        tokenCollection.AddToken(stringBuilder.ToString());
                        stringBuilder.Clear();
                    }
                    continue;
                }

                var current = stringBuilder.ToString();
                var next = current + value;

                if (symbols.Any(v => v.Type == SymbolType.Terminal && v.Value == next))
                {
                    tokenCollection.AddToken(next);
                    stringBuilder.Clear();
                    continue;
                }

                stringBuilder.Append(value);
            }

            if (stringBuilder.Length > 0)
            {
                tokenCollection.AddToken(stringBuilder.ToString());
            }

            return tokenCollection;
        }
    }
}
