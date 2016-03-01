using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace PersonalTimeReport
{
   public class WeekNumberToVisibilityConverter : IMultiValueConverter
   {
      public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
      {
         WorkingDay wd = value[0] as WorkingDay;
         System.Collections.ObjectModel.ObservableCollection<string> VisibleWeeks = value[1] as System.Collections.ObjectModel.ObservableCollection<string>;
         Visibility result = Visibility.Collapsed;
         if (wd != null)
         {
            if (wd.WeekNumber == Utilities.GetWeekNumer(DateTime.Now) || wd.ActualWorkingDay.DayOfWeek == DayOfWeek.Monday || VisibleWeeks.Contains(string.Concat(wd.ActualWorkingDay.Year.ToString(), wd.WeekNumber.ToString().PadLeft(2, '0'))))
            {
               result = Visibility.Visible;
            }
         }
         return result;
      }

      public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
      {
         return null;
      }
   }
}
