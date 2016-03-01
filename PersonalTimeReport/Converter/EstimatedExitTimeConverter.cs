using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace PersonalTimeReport
{
   public class EstimatedExitTimeConverter : IMultiValueConverter
   {
      public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
      {
         TimeSpan? MorningEntrance = value[0] as TimeSpan?;
         TimeSpan? MorningExit = value[1] as TimeSpan?;
         TimeSpan? EveningEntrance = value[2] as TimeSpan?;
         TimeSpan result = TimeSpan.Zero;
         if (MorningEntrance.HasValue)
         {
            if (EveningEntrance.Value != TimeSpan.Zero)
            {
               result = ApplicationManager.Instance.HOUR_TOTAL + EveningEntrance.Value - (MorningExit.Value - MorningEntrance.Value);
            }
            else
            {
               result = ApplicationManager.Instance.HOUR_TOTAL + MorningEntrance.Value;
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
