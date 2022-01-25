using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    /// <summary>
    /// TheState
    /// </summary>
    public sealed class TheState : IEquatable<TheState>
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// TheState
        /// </summary>
        public static implicit operator TheState(string name)
        {
            return new TheState { Name = name };
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TheState other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this.Name, other.Name))
            {
                return true;
            }

            return string.Compare(Name, other.Name, true) == 0;
        }

        /// <summary>
        /// ==
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator ==(TheState first, TheState second)
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
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator !=(TheState first, TheState second)
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
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is TheState)
            {
                return Equals((TheState)obj);
            }

            return false;
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return RuntimeHelpers.GetHashCode(this.Name);
        }
    }
}
