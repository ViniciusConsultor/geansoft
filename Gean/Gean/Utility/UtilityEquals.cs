using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public static class UtilityEquals
    {
        static public bool PairEquals<T>(T a, T b)
        {
            if ((a == null) && (b == null))
                return true;
            else
            {
                if ((a != null) && (b == null)) return false;
                if ((a == null) && (b != null)) return false;
                //至此，a与b均不应是Null值
                if (!a.Equals(b)) return false;
            }
            return true;
        }

        static public bool ListEquals<T>(IList<T> array1, IList<T> array2)
        {
            if (!Object.ReferenceEquals(array1.GetType(), array2.GetType())) return false;
            if ((array1 == null) && (array2 == null))
                return true;
            else
            {
                if ((array1 != null) && (array2 == null)) return false;
                if ((array1 == null) && (array2 != null)) return false;
                //至此，a与b均不应是Null值
                if (array1.Count != array2.Count) return false;
                object v1, v2;
                for (int i = 0; i < array1.Count; i++)
                {
                    v1 = (object)array1[i];
                    v2 = (object)array2[i];
                    if (!v1.Equals(v2))
                        return false;
                }
            }
            return true;
        } 
    }
}
