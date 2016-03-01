using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace PersonalTimeReport
{
   public class CountToVisibilityConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         Visibility result = Visibility.Collapsed;
         int count = 0;
         if (value != null && int.TryParse(string.Concat(value), out count))
         {
            if (count > 0)
            {
               result = Visibility.Visible;
            }
         }
         return result;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return value;
      }
   }
}
