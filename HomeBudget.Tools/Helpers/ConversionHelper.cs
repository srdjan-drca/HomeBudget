using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace HomeBudget.Tools.Helpers {

   public static class ConversionHelper {

      //this constant is implemented as a readonly field instead of a get property for performance reasons
      public static readonly DateTime NullTime = new DateTime(1800, 1, 1);

      public static readonly DateTime MinimalBirthDate = new DateTime(1900, 1, 1);

      public static readonly DateTime MaxValidToDate = new DateTime(2099, 12, 31);

      public static int ToInt(object value) {
         int convertedValue = 0;

         if (CheckHelper.IsFilled(value)) {
            if (value is int || value.GetType().IsEnum) {
               convertedValue = (int)value;
            }
            else {
               string stringValue = value.ToString();
               int.TryParse(stringValue, out convertedValue);
            }
         }

         return convertedValue;
      }

      public static int ToInt(string value) {
         int convertedValue = 0;

         if (CheckHelper.IsFilled(value)) {
            int.TryParse(value, out convertedValue);
         }

         return convertedValue;
      }

      public static int? ToNullableInt(object value) {
         int? convertedValue;

         if (CheckHelper.IsFilled(value)) {
            if (value is int || value.GetType().IsEnum) {
               convertedValue = (int)value;
            }
            else {
               int temp;
               var stringValue = value.ToString();
               int.TryParse(stringValue, out temp);
               convertedValue = temp;
            }
         }
         else {
            convertedValue = null;
         }

         return convertedValue;
      }

      public static string ToString(object value) {
         string stringValue = string.Empty;

         if (CheckHelper.IsFilled(value)) {
            stringValue = value.ToString();
         }

         return stringValue;
      }

      public static string ToString(double value, bool emptyStringForZero = false) {
         string stringValue;

         if (emptyStringForZero && !CheckHelper.IsFilled(value)) {
            stringValue = string.Empty;
         }
         else {
            stringValue = ToLongString(value);
         }

         return stringValue;
      }

      public static string ToStringDateFormat(DateTime? date, string format) {
         string stringValue = string.Empty;

         if (date.HasValue) {
            stringValue = date.Value.ToString(format);
         }

         return stringValue;
      }

      /// <summary>
      /// Method converts object to double
      /// </summary>
      /// <param name="value">Value to be converted </param>
      /// <param name="invariantCulture">True to enable conversion culture-independent (insensitive)</param>
      /// <returns>Converted value</returns>
      public static double ToDouble(object value, bool invariantCulture = false) {
         double convertedValue = 0.0;

         if (CheckHelper.IsFilled(value)) {
            if (value is double) {
               convertedValue = (double)value;
            }
            else {
               string stringValue = value.ToString();
               if (invariantCulture) {
                  double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out convertedValue);
               }
               else {
                  double.TryParse(stringValue, out convertedValue);
               }
            }
         }

         return convertedValue;
      }

      public static double? ToNullableDouble(object value) {
         double? convertedValue;

         if (CheckHelper.IsFilled(value)) {
            if (value is decimal) {
               convertedValue = (double)value;
            }
            else {
               double temp;
               var stringValue = value.ToString();
               double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out temp);
               convertedValue = temp;
            }
         }
         else {
            convertedValue = null;
         }

         return convertedValue;
      }

      public static decimal ToDecimal(object value) {
         decimal convertedValue = 0.0m;

         if (CheckHelper.IsFilled(value)) {
            if (value is decimal) {
               convertedValue = (decimal)value;
            }
            else {
               string stringValue = value.ToString().Replace("'", string.Empty);
               decimal.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out convertedValue);
            }
         }

         return convertedValue;
      }

      public static float ToFloat(object value) {
         float convertedValue = 0.0f;

         if (CheckHelper.IsFilled(value)) {
            if (value is float) {
               convertedValue = (float)value;
            }
            else {
               string stringValue = value.ToString();
               float.TryParse(stringValue, out convertedValue);
            }
         }

         return convertedValue;
      }

      public static int ToDbBoolean(bool value) {
         int convertedValue = 0;

         if (value) {
            convertedValue = -1;
         }

         return convertedValue;
      }

      public static decimal ToPercent(decimal value) {
         decimal convertedValue;

         convertedValue = value * 100m;

         return convertedValue;
      }

      public static byte[] ToBytes(object value) {
         byte[] convertedValue = null;

         if (CheckHelper.IsFilled(value)) {
            convertedValue = value as byte[];
         }

         return convertedValue;
      }

      public static long ToLong(object value) {
         long convertedValue = 0;

         if (CheckHelper.IsFilled(value)) {
            if (value is long) {
               convertedValue = (long)value;
            }
            else {
               string stringValue = value.ToString();
               long.TryParse(stringValue, out convertedValue);
            }
         }

         return convertedValue;
      }

      public static DateTime? ToDateTime(object value, bool invariantCulture = false) {
         DateTime? retVal = null;

         if (CheckHelper.IsFilled(value)) {
            if (value is DateTime) {
               retVal = (DateTime)value;
            }
            else {
               DateTime dateValue;
               if (invariantCulture) {
                  DateTime.TryParse(value.ToString(), DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateValue);
               }
               else {
                  DateTime.TryParse(value.ToString(), out dateValue);
               }

               retVal = dateValue;
            }
         }

         return retVal;
      }

      public static DateTime? ToDateTime(string value, string format, string cultureCode) {
         DateTime? retVal = null;

         if (CheckHelper.IsFilled(value)) {
            DateTime convertedValue;
            if (DateTime.TryParseExact(value, format, CultureInfo.CreateSpecificCulture(cultureCode), DateTimeStyles.None, out convertedValue)) {
               retVal = convertedValue;
            }
         }

         return retVal;
      }

      public static DateTime? ToDateTime(string value, string format) {
         DateTime? retVal = null;

         if (CheckHelper.IsFilled(value)) {
            DateTime convertedValue;
            if (DateTime.TryParseExact(value, format, null, DateTimeStyles.None, out convertedValue)) {
               retVal = convertedValue;
            }
         }

         return retVal;
      }

      public static DateTime ToDBDateTime(DateTime? value) {
         DateTime dateValue;

         if (CheckHelper.IsFilled(value)) {
            dateValue = value.Value;
         }
         else {
            dateValue = NullTime;
         }

         return dateValue;
      }

      public static DateTime? ToApiDateTime(DateTime? value) {
         DateTime date;

         if (value.HasValue && value.Value != NullTime) {
            date = value.Value;
            date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
         }
         else {
            return null;
         }

         return date;
      }

      public static Tuple<DateTime, DateTime> ToDateTimeRange(string daterange, string format) {
         Tuple<DateTime, DateTime> retVal = null;
         var dates = daterange.Split('-');

         if (dates.Length > 1) {
            DateTime? dateFrom = ToDateTime(dates[0].Trim(), format);
            DateTime? dateTo = ToDateTime(dates[1].Trim(), format);

            if (CheckHelper.IsFilled(dateFrom)
               && CheckHelper.IsFilled(dateTo)) {
               retVal = new Tuple<DateTime, DateTime>(dateFrom.Value, dateTo.Value);
            }
         }

         return retVal;
      }

      public static string ToApiString(string value) {
         return CheckHelper.IsFilled(value)
            ? value
            : null;
      }

      public static bool ToBoolean(string value) {
         if (CheckHelper.IsFilled(value) && string.Equals(value, "true", StringComparison.CurrentCultureIgnoreCase)) {
            return true;
         }

         return false;
      }

      public static bool ToBoolean(int value) {
         if (value == 1) {
            return true;
         }

         return false;
      }

      public static bool ToBoolean(object value) {
         bool retVal;

         if (value is bool) {
            retVal = (bool)value;
         }
         else if (value is string) {
            retVal = ToBoolean(ToString(value));
         }
         else {
            retVal = ToBoolean(ToInt(value));
         }

         return retVal;
      }

      public static T? ToEnum<T>(object value) where T : struct, IConvertible {
         T result;
         if (CheckHelper.IsFilled(value) && Enum.TryParse(value.ToString(), true, out result)) {
            return result;
         }

         return null;
      }

      public static string ByteArrayToString(byte[] byteArray) {
         UTF8Encoding enc = new UTF8Encoding();
         return enc.GetString(byteArray);
      }

      public static object IfNullReturnDBNullValue(object value) {
         if (CheckHelper.IsFilled(value)) {
            return value;
         }

         return DBNull.Value;
      }

      public static object IfDBNullReturnDefaultValue(object value, object defaultValue) {
         if (value != DBNull.Value) {
            return value;
         }

         return defaultValue;
      }

      public static object IfNullReturnNull(object value) {
         if (CheckHelper.IsFilled(value)) {
            return value;
         }

         return null;
      }

      /// <summary>
      /// Method converts a dictionary to a list of objects that have key, value properties.
      /// Good for json conversion.
      /// </summary>
      public static List<object> DictionaryToListOfObjects(IDictionary<string, string> dict) {
         List<object> objects;
         objects = new List<object>(dict.Count);

         foreach (var pair in dict) {
            object obj = new { key = pair.Key, value = pair.Value };
            objects.Add(obj);
         }

         return objects;
      }

      /// <summary>
      /// Method converts a dictionary to a list of objects that have key, value properties.
      /// Good for json conversion.
      /// </summary>
      public static List<object> DictionaryToListOfObjects(IDictionary<string, Tuple<string, string>> dict) {
         List<object> objects;
         objects = new List<object>(dict.Count);

         foreach (var pair in dict) {
            object obj = new { key = pair.Key, value = pair.Value.Item1, @ref = pair.Value.Item2 };
            objects.Add(obj);
         }

         return objects;
      }

      public static string ByteArrayToHexString(byte[] value) {
         string retVal = string.Empty;
         if (CheckHelper.IsFilled(value)) {
            retVal = string.Concat(Array.ConvertAll(value, x => x.ToString("x2")));
         }

         return retVal;
      }

      public static byte[] HexStringToByteArray(string value) {
         byte[] retVal = null;
         if (CheckHelper.IsFilled(value)) {
            int numberChars = value.Length;
            retVal = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2) {
               retVal[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            }
         }

         return retVal;
      }

      public static DateTime ToDBDateTime(object value) {
         return ToDBDateTime(ToDateTime(value));
      }

      /// <summary>
      /// This method covers the case when dateTime has min value.
      /// It will convert it to NullTime (default value).
      /// </summary>
      public static DateTime ToDBDateTime(DateTime dateTime) {
         return ToDBDateTime(ToDateTime(dateTime));
      }

      public static object ToEnumObject(Type type, object value) {
         return Enum.ToObject(type, ToInt(value));
      }

      #region Private

      private static string ToLongString(double input) {
         string str = input.ToString().ToUpper();

         // if string representation was collapsed from scientific notation, just return it:
         if (!str.Contains("E")) {
            return str;
         }

         bool negativeNumber = false;

         if (str[0] == '-') {
            str = str.Remove(0, 1);
            negativeNumber = true;
         }

         string sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
         char decSeparator = sep.ToCharArray()[0];

         string[] exponentParts = str.Split('E');
         string[] decimalParts = exponentParts[0].Split(decSeparator);

         // fix missing decimal point:
         if (decimalParts.Length == 1) {
            decimalParts = new string[] { exponentParts[0], "0" };
         }

         int exponentValue = int.Parse(exponentParts[1]);

         string newNumber = decimalParts[0] + decimalParts[1];

         string result;

         if (exponentValue > 0) {
            result =
                newNumber +
                GetZeros(exponentValue - decimalParts[1].Length);
         }
         else {
            // negative exponent
            result =
                "0" +
                decSeparator +
                GetZeros(exponentValue + decimalParts[0].Length) +
                newNumber;

            result = result.TrimEnd('0');
         }

         if (negativeNumber) {
            result = "-" + result;
         }

         return result;
      }

      private static string GetZeros(int zeroCount) {
         if (zeroCount < 0) {
            zeroCount = Math.Abs(zeroCount);
         }

         return new string('0', zeroCount);
      }

      #endregion Private
   }
}