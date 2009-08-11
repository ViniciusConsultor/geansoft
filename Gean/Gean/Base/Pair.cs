using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Gean
{
    /// <summary>
    /// 一个存储了一对值（非键值对类型）的结构（struct）。值均不能为Null。
    /// 该结构重写了==和!=操作符。
    /// Gean:2009-06-06 11:02:08
    /// </summary>
    [Serializable]
    public struct Pair<A, B> : IEquatable<Pair<A, B>>, ISerializable
        where A : IEquatable<A>
        where B : IEquatable<B>
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
            if (obj is Pair<A, B>)
                return Equals((Pair<A, B>)obj); // use Equals method below
            else
                return false;
        }

        public bool Equals(Pair<A, B> other)
        {
            // add comparisions for all members here
            return First.Equals(other.First) && Second.Equals(other.Second);
        }

        public override int GetHashCode()
        {
            // combine the hash codes of all members here (e.g. with XOR operator ^)
            return unchecked(3 * (First.GetHashCode() + Second.GetHashCode()));
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

}
