using System;
using System.Collections.Generic;

namespace Panosen.Compling
{
    /// <summary>
    /// TokenCollection
    /// </summary>
    public class TokenCollection
    {
        /// <summary>
        /// token list
        /// </summary>
        public List<Token> TokenList { get; set; }

        /// <summary>
        /// this
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Token this[int index]
        {
            get
            {
                return this.TokenList[index];
            }
        }
    }

    /// <summary>
    /// TokenCollectionExtension
    /// </summary>
    public static class TokenCollectionExtension
    {
        /// <summary>
        /// token count
        /// </summary>
        /// <param name="tokenCollection"></param>
        /// <returns></returns>
        public static int Count(this TokenCollection tokenCollection)
        {
            if (tokenCollection.TokenList == null)
            {
                return 0;
            }

            return tokenCollection.TokenList.Count;
        }

        /// <summary>
        /// AddToken
        /// </summary>
        public static Token AddToken(this TokenCollection tokenCollection, string value = null, int row = 0, int col = 0)
        {
            if (tokenCollection.TokenList == null)
            {
                tokenCollection.TokenList = new List<Token>();
            }

            Token token = new Token();
            token.Value = value;
            token.Row = row;
            token.Col = col;

            tokenCollection.TokenList.Add(token);

            return token;
        }
    }
}
