using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Gean
{
    /// <summary>
    /// 一个存储了三个值（非键值对类型）的类型。
    /// 该结构重写了==和!=操作符。
    /// Gean: 2009-08-24 22:30:51
    /// </summary>
    [Serializable]
    public class Triplet<A, B, C> : ISerializable
    {
        public readonly A First;
        public readonly B Second;
        public readonly C Third;

        public Triplet(A first, B second, C third)
        {
            if (first == null)
                throw new ArgumentNullException("first");
            if (second == null)
                throw new ArgumentNullException("second");
            if (third == null)
                throw new ArgumentNullException("third");
            this.First = first;
            this.Second = second;
            this.Third = third;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;
            if (obj is Triplet<A, B, C>)
                return this.Equals((Triplet<A, B, C>)obj);
            else
                return false;
        }

        public bool Equals(Triplet<A, B, C> other)
        {
            return First.Equals(other.First) && Second.Equals(other.Second) && Third.Equals(other.Third);
        }

        public override int GetHashCode()
        {
            return unchecked(3 * (First.GetHashCode() ^ Second.GetHashCode() ^ Third.GetHashCode()));
        }

        public override string ToString()
        {
            string str = string.Format("[First: {0}][Second: {1}][Third: {2}]", First, Second, Third);
            return str;
        }

        public static bool operator ==(Triplet<A, B, C> lhs, Triplet<A, B, C> rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Triplet<A, B, C> lhs, Triplet<A, B, C> rhs)
        {
            return !(lhs.Equals(rhs));
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
            info.AddValue("ThirdValue", this.Third);
        }
    }
}
