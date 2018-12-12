

namespace ZacksSampleCode.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// This extension is intended to perform operations on members of an <see cref="IEnumerable"/> when side effects 
        /// on the members are desired. This method enumerates the collection. Side effects should be removed from 
        /// other <see cref="IEnumerable"/> operations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="action"></param>
        /// <returns><see cref="IEnumerable"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<T> Do<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (action == null) throw new ArgumentNullException("action");
            foreach (T item in items)
            {
                action(item);
            }
            return items;
        }

        /// <summary>
        /// Makes ForEach availiable to IEnumerable.  This enumerates the collection and breaks 
        /// deferred evaluation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="action"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (action == null) throw new ArgumentNullException("action");
            foreach (T item in items)
                action(item);
        }

        /// <summary>
        /// Makes ForEach availiable to IEnumerable with an enumeration count.  This enumerates the collection and breaks 
        /// deferred evaluation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="action"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T, int> action)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (action == null) throw new ArgumentNullException("action");
            int i = 0;
            foreach (T item in items)
            {
                action(item, i);
                i++;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> items, int seed, Action<T, int> action)
        {
            foreach (T item in items)
            {
                action(item, seed);
                seed++;
            }
        }

        /// <summary>
        /// Performes a full outer join and groups elements according to the key selector functions and the projection 
        /// function. This method enumerates each collection.
        /// </summary>
        /// <typeparam name="TA">Type of element in IEnumerable a</typeparam>
        /// <typeparam name="TB">Type of element in IEnumerable b</typeparam>
        /// <typeparam name="TKey">Type of the key on which the enumerables will be joined.</typeparam>
        /// <typeparam name="TR">Type of element in the enumerable returned by the join.</typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="selectKeyA">selection function that selects the key value from <paramref name="a"/></param>
        /// <param name="selectKeyB">selection function that selects the key value from <paramref name="b"/></param>
        /// <param name="projection">Projection determines the type of output.  It takes a list of objects from each of the source arrays
        /// that match the selection criteria and can return an array so this function will return an enumerable of arrays or create a 
        /// single item out of the lists.</param>
        /// <param name="cmp"></param>
        /// <returns></returns>
        /// <remarks>since there may be more than one item corresponding to each key in each sequence the projection function 
        /// is used to select the output from each sequence.
        /// </remarks>
        public static IEnumerable<TR> FullOuterGroupJoin<TA, TB, TKey, TR>(
            this IEnumerable<TA> a,
            IEnumerable<TB> b,
            Func<TA, TKey> selectKeyA,
            Func<TB, TKey> selectKeyB,
            Func<IEnumerable<TA>, IEnumerable<TB>, TKey, TR> projection,
            IEqualityComparer<TKey> cmp = null)
        {
            cmp = cmp ?? EqualityComparer<TKey>.Default;
            var alookup = a.ToLookup(selectKeyA, cmp);
            var blookup = b.ToLookup(selectKeyB, cmp);

            var keys = new HashSet<TKey>(alookup.Select(p => p.Key), cmp);
            keys.UnionWith(blookup.Select(p => p.Key));

            var join = from key in keys
                       let xa = alookup[key]
                       let xb = blookup[key]
                       select projection(xa, xb, key);

            return join.ToList();
        }

        public static IEnumerable<TR> FullOuterGroupJoin<T, TKey, TR>(
                   this IEnumerable<T> list,
                   IEnumerable<T> items,
                   Func<T, TKey> selectKey,
                   Func<IEnumerable<T>, IEnumerable<T>, TKey, TR> projection,
                   IEqualityComparer<TKey> cmp = null)
        {
            return list.FullOuterGroupJoin(items, selectKey, selectKey, projection, cmp);
        }
        public static IEnumerable<TR> FullOuterJoin<TA, TB, TKey, TR>(
            this IEnumerable<TA> a,
            IEnumerable<TB> b,
            Func<TA, TKey> selectKeyA,
            Func<TB, TKey> selectKeyB,
            Func<TA, TB, TKey, TR> projection,
            TA defaultA = default(TA),
            TB defaultB = default(TB),
            IEqualityComparer<TKey> cmp = null)
        {
            cmp = cmp ?? EqualityComparer<TKey>.Default;
            var alookup = a.ToLookup(selectKeyA, cmp);
            var blookup = b.ToLookup(selectKeyB, cmp);

            var keys = new HashSet<TKey>(alookup.Select(p => p.Key), cmp);
            keys.UnionWith(blookup.Select(p => p.Key));

            IEnumerable<TR> join = from key in keys
                                   from xa in alookup[key].DefaultIfEmpty(defaultA)
                                   from xb in blookup[key].DefaultIfEmpty(defaultB)
                                   select projection(xa, xb, key);

            return join.ToList();
        }


        /// <summary>
        /// Takes each element that matches take(T) until done(T) = true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="take"></param>
        /// <param name="done"></param>
        /// <returns></returns>
        public static IEnumerable<T> TakeWhile<T>(this IEnumerable<T> items, Predicate<T> take, Predicate<T> done)
        {
            foreach (T item in items)
            {
                if (done(item))
                    break;
                else if (take(item))
                    yield return item;
            }
        }

        public static IEnumerable<IEnumerable<T>> ToPages<T>(this IEnumerable<T> items, int PageSize)
        {
            if (PageSize == 0)
                PageSize = 1;
            int pageNumber = 0;
            int pageEnd = 0;

            int itemCount = items.Count();


            List<List<T>> pages = new List<List<T>>();

            while (pageEnd < itemCount)
            {
                pages.Add(items.Skip(pageEnd).Take(PageSize).ToList());
                pageNumber++;
                pageEnd = pageNumber * PageSize;
            }
            return pages;



        }



        /// <summary>
        /// Returns a bool indicating whether the sequence has any duplicates.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static bool HasDuplicates<T>(this IEnumerable<T> sequence)
        {
            var set = new HashSet<T>();
            return !sequence.All(item => set.Add(item));
        }
        /// <summary>
        /// Creates a SortedList.  SortedLists use less memory than a SortedDictionary and are faster when inserting sorted data.
        /// Use a SortedDictionary for unsorted data.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="items"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static SortedList<TKey, TSource> ToSortedList<TSource, TKey>(this IEnumerable<TSource> items, Func<TSource, TKey> keySelector)
        {
            return items.ToSortedList(keySelector, (s) => s);
        }

        /// <summary>
        /// Creates a SortedList.  SortedLists use less memory than a SortedDictionary and are faster when inserting sorted data.
        /// Use a SortedDictionary for unsorted data.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="items"></param>
        /// <param name="keySelector"></param>
        /// <param name="elementSelector">Transform function that projects TSource to TElement</param>
        /// <returns></returns>
        public static SortedList<TKey, TElement> ToSortedList<TSource, TElement, TKey>(this IEnumerable<TSource> items, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            SortedList<TKey, TElement> sortedList = new SortedList<TKey, TElement>();
            foreach (TSource item in items)
            {
                sortedList.Add(keySelector(item), elementSelector(item));
            }
            return sortedList;
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> items, Func<TSource, bool> predicate, TSource Default)
        {
            if (items.Any(predicate))
            {
                return items.FirstOrDefault(predicate);
            }
            else
                return Default;
        }


        public static IEnumerable<T> Except<T>(this IEnumerable<T> items, IEnumerable<T> second, Func<T, T, bool> equality)
        {
            foreach (T item in items)
            {
                if (!second.Any(s => equality(item, s)))
                    yield return item;
            }
        }
    }
   
}
