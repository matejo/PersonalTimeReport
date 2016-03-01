using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace PersonalTimeReport
{
   public class NullToVisibilityConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         Visibility result = Visibility.Visible;
         if (string.IsNullOrEmpty(string.Concat(value)))
         {
            result = Visibility.Collapsed;
         }
         return result;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return value;
      }
   }
}
