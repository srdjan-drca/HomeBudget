using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeBudget.Tools.Helpers {

   public static class CheckHelper {
      private static readonly Type stringType = typeof(string);
      private static readonly Type intType = typeof(int);
      private static readonly Type doubleType = typeof(double);
      private static readonly Type floatType = typeof(float);
      private static readonly Type decimalType = typeof(decimal);
      private static readonly Type dateTimeType = typeof(DateTime);

      public static bool IsFilled(string value) {
         bool result = !string.IsNullOrWhiteSpace(value);

         return result;
      }

      public static bool IsFilled(int value) {
         bool result = value != 0;

         return result;
      }

      public static bool IsFilled(long value) {
         bool result = value != 0L;

         return result;
      }

      public static bool IsFilled(DateTime value) {
         bool result = (value != DateTime.MinValue) && (value > ConversionHelper.NullTime);

         return result;
      }

      public static bool IsFilled(DateTime? value) {
         bool result = value.HasValue && IsFilled(value.Value);

         return result;
      }

      public static bool IsFilled(int? value) {
         bool result = value.HasValue && IsFilled(value.Value);

         return result;
      }

      public static bool IsFilled(float? value) {
         bool result = value.HasValue && IsFilled(value.Value);

         return result;
      }

      public static bool IsFilled(double? value) {
         bool result = value.HasValue && IsFilled(value.Value);

         return result;
      }

      public static bool IsFilled(decimal? value) {
         bool result = value.HasValue && IsFilled(value.Value);

         return result;
      }

      public static bool IsFilled(float value) {
         bool result = !(Math.Abs(value) < float.Epsilon);

         return result;
      }

      public static bool IsFilled(double value) {
         bool result = !(Math.Abs(value) < double.Epsilon);

         return result;
      }

      public static bool IsFilled(decimal value) {
         bool result = value != 0.0m;

         return result;
      }

      public static bool IsFilled(IComparable value) {
         return IsFilled((object)value);
      }

      public static bool IsFilled<T>(T? value) where T : struct {
         return value != null;
      }

      public static bool IsFilled<T>(T value) where T : class {
         return value != null;
      }

      public static bool IsFilled(object value) {
         bool result = false;

         if (value != null && value != DBNull.Value) {
            Type valueType = value.GetType();

            if (valueType == stringType) {
               result = IsFilled((string)value);
            }
            else if (valueType == intType) {
               result = IsFilled((int)value);
            }
            else if (valueType == doubleType) {
               result = IsFilled((double)value);
            }
            else if (valueType == floatType) {
               result = IsFilled((float)value);
            }
            else if (valueType == decimalType) {
               result = IsFilled((decimal)value);
            }
            else if (valueType == dateTimeType) {
               result = IsFilled((DateTime)value);
            }
            else {
               result = true;
            }
         }

         return result;
      }

      public static bool IsFilled(Stream value) {
         return IsFilled((object)value) && !value.GetType().IsInstanceOfType(Stream.Null);
      }

      public static bool IsEqual(object valueA, object valueB) {
         bool isFilledA = IsFilled(valueA);
         bool isFilledB = IsFilled(valueB);

         //fields must be either both empty or both filled and equals
         bool isEqual = (!isFilledA && !isFilledB) || (isFilledA && isFilledB && Equals(valueA, valueB));
         return isEqual;
      }

      /// <summary>
      /// Check if a type inherits a generic type
      /// </summary>
      public static bool IsSubclassOfRawGeneric(Type genericType, Type toCheckType) {
         bool isValid = false;

         while (toCheckType != null && toCheckType != typeof(object)) {
            Type current = toCheckType.IsGenericType ? toCheckType.GetGenericTypeDefinition() : toCheckType;
            if (genericType == current) {
               isValid = true;
               break;
            }

            toCheckType = toCheckType.BaseType;
         }

         return isValid;
      }

      public static bool DoesListContainDuplicates<T>(List<T> list) {
         HashSet<T> set = new HashSet<T>();
         return list.Any(value => !set.Add(value));
      }

      public static bool IsStringChanged(string viewValue, string dataValue) {
         bool isChanged = false;

         if (!IsFilled(dataValue)) {
            if (IsFilled(viewValue)) {
               isChanged = true;
            }
         }
         else {
            isChanged = !dataValue.Equals(viewValue);
         }

         return isChanged;
      }
   }
}