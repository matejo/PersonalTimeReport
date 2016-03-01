using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PersonalTimeReport
{
   public class TodayToBackgroundConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         object result = Colors.Transparent;
         DateTime? ActualWorkingDay = value as DateTime?;
         if (ActualWorkingDay.HasValue)
         {
            if (ActualWorkingDay.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
            {
                result = (RadialGradientBrush)Application.Current.Resources["TodayBackground"];
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
