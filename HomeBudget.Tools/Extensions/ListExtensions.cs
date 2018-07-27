using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeBudget.Tools.Extensions {

   public static class ListExtensions {

      public static List<TType> MoveToTop<TType>(this IEnumerable<TType> list, Func<TType, bool> predicate) {
         return list.Where(predicate).Concat(list.Where(item => !predicate(item))).ToList();
      }
   }
}