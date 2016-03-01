using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace PersonalTimeReport
{
   public class DayToStyleConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         object style = Application.Current.Resources["FontSmall"];
         WorkingDay wd = value as WorkingDay;
         if (wd != null)
         {
            Settings settings = (ApplicationManager.Instance.Managers["SettingsManager"] as SettingsManager).Settings;
            bool IsSaturdayHoliday = false;
            bool.TryParse(settings["IsSaturdayHoliday"].Value, out IsSaturdayHoliday);
            if (string.Concat(parameter).ToLower() != "number")
            {
               if (wd.ActualWorkingDay.DayOfWeek == DayOfWeek.Sunday)
               {
                  style = Application.Current.Resources["FontSmallHoliday"];
               }
               else if (wd.ActualWorkingDay.DayOfWeek == DayOfWeek.Saturday && IsSaturdayHoliday)
               {
                  style = Application.Current.Resources["FontSmallHoliday"];
               }
               else
               {
                  style = Application.Current.Resources["FontSmall"];
               }
            }
            else
            {
               if (wd.ActualWorkingDay.DayOfWeek == DayOfWeek.Sunday)
               {
                  style = Application.Current.Resources["FontMediumHoliday"];
               }
               else if (wd.ActualWorkingDay.DayOfWeek == DayOfWeek.Saturday && IsSaturdayHoliday)
               {
                  style = Application.Current.Resources["FontMediumHoliday"];
               }
               else
               {
                  style = Application.Current.Resources["FontMedium"];
               }
            }
         }
         return style;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return null;
      }
   }
}
