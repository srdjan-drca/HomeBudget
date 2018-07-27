using System;

namespace HomeBudget.Tools.Helpers {

   public static class MonadHelper {

      /// <summary>
      /// Method is used so we don't have to write multiple if statements when checking for nulls
      /// </summary>
      /// <typeparam name="TInput"></typeparam>
      /// <typeparam name="TResult"></typeparam>
      /// <param name="obj"></param>
      /// <param name="evaluator"></param>
      /// <returns></returns>
      public static TResult With<TInput, TResult>(this TInput obj, Func<TInput, TResult> evaluator)
         where TInput : class {
         if (CheckHelper.IsFilled(obj)) {
            return evaluator(obj);
         }

         return default(TResult);
      }
   }
}