using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Gean
{
    /// <summary>
    /// 一个存储了一对值（非键值对类型）的类型。
    /// 移植自System.Data.Common.Utils的一个内部类。
    /// </summary>
    /// <typeparam name="TFirst">The type of the first.</typeparam>
    /// <typeparam name="TSecond">The type of the second.</typeparam>
    public class Pair<TFirst, TSecond>
    {
        private TFirst first;
        private TSecond second;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pair&lt;TFirst, TSecond&gt;"/> class.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        public Pair(TFirst first, TSecond second)
        {
            this.first = first;
            this.second = second;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Pair<TFirst, TSecond> other)
        {
            return (this.first.Equals(other.first) && this.second.Equals(other.second));
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object other)
        {
            Pair<TFirst, TSecond> pair = other as Pair<TFirst, TSecond>;
            return ((pair != null) && this.Equals(pair));
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return ((this.first.GetHashCode() << 5) ^ this.second.GetHashCode());
        }

        /// <summary>
        /// Gets the first.
        /// </summary>
        /// <value>The first.</value>
        public TFirst First
        {
            get { return this.first; }
            set { this.first = value; }
        }

        /// <summary>
        /// Gets the second.
        /// </summary>
        /// <value>The second.</value>
        public TSecond Second
        {
            get { return this.second; }
            set { this.second = value; }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<");
            builder.Append(this.first.ToString());
            builder.Append(", " + this.second.ToString());
            builder.Append(">");
            return builder.ToString();
        }
    }

    /*
    [Serializable]
    public class Pair<A, B> : ISerializable
    {
        public readonly A First;
        public readonly B Second;

        public Pair(A first, B second)
        {
            if (first == null)
                throw new ArgumentNullException("first");
            if (second == null)
                throw new ArgumentNullException("second");
            this.First = first;
            this.Second = second;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;
            if (obj is Pair<A, B>)
                return Equals((Pair<A, B>)obj);
            else
                return false;
        }

        public bool Equals(Pair<A, B> other)
        {
            return First.Equals(other.First) && Second.Equals(other.Second);
        }

        public override int GetHashCode()
        {
            return unchecked(27 * (First.GetHashCode() ^ Second.GetHashCode()));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            return sb.Append(First.ToString()).Append(" | ").Append(Second.ToString()).ToString();
        }

        public static bool operator ==(Pair<A, B> lhs, Pair<A, B> rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Pair<A, B> lhs, Pair<A, B> rhs)
        {
            return !(lhs.Equals(rhs)); // use operator == and negate result
        }

        [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info is null.");
            }
            info.AddValue("FirstValue", this.First);
            info.AddValue("SecondValue", this.Second);
        }
    }
    */
}