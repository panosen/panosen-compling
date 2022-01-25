using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{

    /// <summary>
    /// 符号
    /// </summary>
    public sealed class Symbol : IEquatable<Symbol>
    {
        /// <summary>
        /// 符号类型
        /// </summary>
        public SymbolType Type { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        #region IEquatable<Symbol> Members

        /// <summary>
        /// Equals
        /// </summary>
        public bool Equals(Symbol other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (this.Type != other.Type)
            {
                return false;
            }

            if (string.Compare(this.Value, other.Value, false) != 0)
            {
                return false;
            }

            return true;
        }

        #endregion

        /// <summary>
        /// ==
        /// </summary>
        public static bool operator ==(Symbol first, Symbol second)
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

        /// <summary>
        /// !=
        /// </summary>
        public static bool operator !=(Symbol first, Symbol second)
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

        /// <summary>
        /// Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is Symbol)
            {
                return Equals((Symbol)obj);
            }

            return false;
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            return (int)this.Type ^ RuntimeHelpers.GetHashCode(this.Value);
        }
    }
}
