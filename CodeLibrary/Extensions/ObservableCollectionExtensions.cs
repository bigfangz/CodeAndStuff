

namespace ZacksSampleCode.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> sequense, IEnumerable<T> range)
        {
            foreach (var i in range) { sequense.Add(i); }
        }

        /// <summary>
        /// Returns the index of the first element that meets the predicate criteria. If there are no elements
        /// in the sequence that meet the predicate criteria this method returns -1;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequense"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int FindIndex<T>(this ObservableCollection<T> sequense, Predicate<T> predicate)
        {
            int indx = -1;
            int i = 0;
            foreach (T item in sequense)
            {
                if (predicate(item))
                {
                    indx = i;
                    break;
                }
                i++;
            }
            return indx;
        }
        public static IEnumerable<T> Where<T>(this ObservableCollection<T> sequense, Predicate<T> predicate)
        {
            foreach (T item in sequense)
                if (predicate(item))
                    yield return item;
        }



    }
}
