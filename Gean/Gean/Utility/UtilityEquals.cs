﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

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

        static public bool EnumerableEquals(IEnumerable a, IEnumerable b)
        {
            if ((a == null) && (b == null))
                return true;
            if (!Object.ReferenceEquals(a.GetType(), b.GetType())) return false;
            else
            {
                if ((a != null) && (b == null)) return false;
                if ((a == null) && (b != null)) return false;
                //至此，a与b均不应是Null值
                IEnumerator e1 = a.GetEnumerator();
                IEnumerator e2 = b.GetEnumerator();
                for (int i = 0; e1.MoveNext() && e2.MoveNext(); i++)
                {
                    if (!e1.Current.Equals(e2.Current))
                        return false;
                }
            }
            return true;
        }

        public static bool CollectionsEquals<T>(ICollection<T> a, ICollection<T> b, IComparer<T> comparer)
        {
            if (!object.ReferenceEquals(a, b))
            {
                if ((a == null) || (b == null))
                {
                    return false;
                }
                if (a.Count != b.Count)
                {
                    return false;
                }
                IEnumerator e1 = a.GetEnumerator();
                IEnumerator e2 = b.GetEnumerator();
                for (int i = 0; e1.MoveNext() && e2.MoveNext(); i++)
                {
                    if (0 != comparer.Compare((T)e1.Current, (T)e2.Current))
                    {
                        return false;
                    }
                }
                return true;
            }
            return true;
        }

        public static bool CollectionsEquals<T>(ICollection<T> expected, ICollection<T> actual)
        {
            return CollectionsEquals(expected, actual, null);
        }

    }
}
