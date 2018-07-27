using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using HomeBudget.Tools.Helpers;

namespace HomeBudget.Converters {

   public class IsEnabledConverter : IMultiValueConverter {

      public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
         var isEnabled = values.All(x => ConversionHelper.ToInt(x) != 0);

         return isEnabled;
      }

      public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
         throw new NotImplementedException();
      }
   }
}