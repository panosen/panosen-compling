using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    /// <summary>
    /// TokenType
    /// </summary>
    public sealed class TokenType : IEquatable<TokenType>
    {
        public string Name { get; set; }

        public static implicit operator TokenType(string name)
        {
            return new TokenType { Name = name };
        }

        public bool Equals(TokenType other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this.Name, other.Name))
            {
                return true;
            }

            return string.Compare(Name, other.Name, true) == 0;
        }

        public static bool operator ==(TokenType first, TokenType second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null))
            {
                return ReferenceEquals(second, null);
            }

            return first.Equals(second);
        }

        public static bool operator !=(TokenType first, TokenType second)
        {
            if (ReferenceEquals(first, second))
            {
                return false;
            }

            if (!ReferenceEquals(first, null))
            {
                return !first.Equals(second);
            }

            return !second.Equals(first);
        }

        public override bool Equals(object obj)
        {
            if (obj is TokenType)
            {
                return Equals((TokenType)obj);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (this.Name == null)
            {
                return 0;
            }

            return this.Name.ToLower().GetHashCode();
        }
    }
}
