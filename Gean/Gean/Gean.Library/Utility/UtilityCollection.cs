using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;

namespace Gean
{
    public class UtilityCollection
    {

        /// <summary>
        /// Runs an action for all elements in the input.
        /// </summary>
        public static void ForEach<T>(IEnumerable<T> input, Action<T> action)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            foreach (T element in input)
            {
                action(element);
            }
        }

        /// <summary>
        /// Adds all 
        /// <paramref name="elements"/> to <paramref name="list"/>.
        /// </summary>
        public static void AddRange<T>(ICollection<T> list, IEnumerable<T> elements)
        {
            foreach (T o in elements)
                list.Add(o);
        }

        public static ReadOnlyCollection<T> AsReadOnly<T>(T[] arr)
        {
            return Array.AsReadOnly(arr);
        }

    }
}
