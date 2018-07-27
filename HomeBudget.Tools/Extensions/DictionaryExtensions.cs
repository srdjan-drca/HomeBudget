using System;
using System.Collections.Generic;

namespace HomeBudget.Tools.Extensions {

   public static class DictionaryExtensions {

      public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> dicToAdd) {
         dicToAdd.ForEach(x => dic.Add(x.Key, x.Value));
      }

      public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
         foreach (var item in source)
            action(item);
      }

      /// <summary>
      /// Extension method for dictionary which get value for passed key.
      /// In case key is not found default value can be specified for return by setting defaultIfNotFound.
      /// If default value is not specified null is returned for reference types and value otherwise (eg. zero for integer).
      /// </summary>
      public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultIfNotFound = default(TValue)) {
         TValue value;

         if (dictionary == null) {
            throw new Exception("Dictionary is not created");
         }

         if (!dictionary.TryGetValue(key, out value)) {
            value = defaultIfNotFound;
         }

         return value;
      }
   }
}