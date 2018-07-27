using System;
using System.Collections.Generic;
using System.Reflection;

namespace HomeBudget.Tools.Helpers {

   /// <summary>
   /// This class is used for dynamic runtime overload resolution in filter mappers (see comparison helper).
   /// It should not be used outside filter mappers.
   /// </summary>
   public class TypeHelper {
      //Note : there is no built-in support in C# to check if a given type can be assigned to another type, with support of generics.
      //Eg : checking that List<int> can be assigned to List<T> with T as int
      //The methods Type.IsAssignableFrom() and Binder.SelectMethod() from .NET framework cannot be used for the reason explained above.

      /// <summary>
      /// Determines whether an instance of a specified type can be assigned to another type instance.
      /// </summary>
      /// <param name="genericArguments">If given type is assignable and base type is a generic type, matching type / generic type pairs are added to the dictionnary</param>
      public static bool IsAssignableFrom(Type givenType, Type baseType, Dictionary<Type, Type> genericArguments) {
         //checks if given type derives from the base type
         Type currentType = givenType;
         while (currentType != null) {
            if (CheckTypesEquality(currentType, baseType, genericArguments)) {
               return true;
            }

            currentType = currentType.BaseType;
         }

         if (baseType.IsInterface) {
            //check if given type implements the specified interface
            currentType = givenType;
            while (currentType != null) {
               foreach (Type interfaceType in currentType.GetInterfaces()) {
                  if (CheckTypesEquality(interfaceType, baseType, genericArguments)) {
                     return true;
                  }
               }

               currentType = currentType.BaseType;
            }
         }
         else if (baseType.IsGenericParameter) {
            //check that all "where" constraints of the generic parameter are valid
            foreach (Type constraint in baseType.GetGenericParameterConstraints()) {
               if (!IsAssignableFrom(givenType, constraint, genericArguments)) {
                  return false;
               }
            }

            //if a generic argument has already been seen, it must be for the same type as before
            if (genericArguments.TryGetValue(baseType, out currentType) && currentType != givenType) {
               return false;
            }

            genericArguments[baseType] = givenType;
            return true;
         }

         return false;
      }

      /// <summary>
      /// Determines whether two types are equals, with support for generic types.
      /// </summary>
      private static bool CheckTypesEquality(Type givenType, Type baseType, Dictionary<Type, Type> genericArguments) {
         if (givenType == baseType) {
            return true;
         }
         else if (givenType.IsGenericType && baseType.IsGenericType) {
            //eg : IList<int> vs IList<T>
            if (givenType.GetGenericTypeDefinition() == baseType.GetGenericTypeDefinition()) {
               Type[] givenTypeArgs = givenType.GetGenericArguments();
               Type[] baseTypeArgs = baseType.GetGenericArguments();

               //checks that all generic arguments are assignable
               for (int i = 0; i < baseTypeArgs.Length; i++) {
                  if (!IsAssignableFrom(givenTypeArgs[i], baseTypeArgs[i], genericArguments)) {
                     return false;
                  }
               }

               return true;
            }
         }

         return false;
      }

      public static MethodInfo GetMethodInfo<T>(Action<T> func) {
         return GetMethodInfo((Delegate)func);
      }

      public static MethodInfo GetMethodInfo<T, TResult>(Func<T, TResult> func) {
         return GetMethodInfo((Delegate)func);
      }

      public static MethodInfo GetMethodInfo<T1, T2, TResult>(Func<T1, T2, TResult> func) {
         return GetMethodInfo((Delegate)func);
      }

      public static MethodInfo GetMethodInfo<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func) {
         return GetMethodInfo((Delegate)func);
      }

      public static MethodInfo GetMethodInfo<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func) {
         return GetMethodInfo((Delegate)func);
      }

      public static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func) {
         return GetMethodInfo((Delegate)func);
      }

      public static MethodInfo GetMethodInfo<T1>(Func<T1, T1> func) {
         return GetMethodInfo((Delegate)func);
      }

      /// <summary>
      /// Gets the method info of a method passed in parameters.
      /// If a generic method is passed, the generic method definition is returned.
      /// </summary>
      private static MethodInfo GetMethodInfo(Delegate del) {
         MethodInfo method = del.Method;
         if (method.IsGenericMethod) {
            method = method.GetGenericMethodDefinition();
         }

         return method;
      }
   }
}