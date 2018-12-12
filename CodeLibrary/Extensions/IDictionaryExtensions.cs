
namespace ZacksSampleCode.Extensions
{
    using System.Collections.Generic;
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Extension to get a value out of a dictionary without having to check for key existance.   Deals with 
        /// that pesky exception when you try to get a value out of a dictionary using the indexing operator.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value = default(TValue);
            dictionary.TryGetValue(key, out value);

            return value;
        }

        public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> range)
        {
            if (range == null || range.Count == 0)
                return dictionary;
            foreach (var kvp in range)
                dictionary.Add(kvp);
            return dictionary;
        }

        //public static ILookUp<TKey,TValue> ToLookup<TKey, TValue>(this IDictionary<TKey,IEnumerable<TValue>> dictionary )
        //{
        //    return null;
        //}
    }
}
