using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace PersonalTimeReport
{
   public class OvertimeConverter : IMultiValueConverter
   {
      public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
      {
         TimeSpan? MorningEntrance = value[0] as TimeSpan?;
         TimeSpan? MorningExit = value[1] as TimeSpan?;
         TimeSpan? EveningEntrance = value[2] as TimeSpan?;
         TimeSpan? EveningExit = value[3] as TimeSpan?;
         TimeSpan result = Utilities.CalculateOvertime(MorningEntrance, MorningExit, EveningEntrance, EveningExit);
         ApplicationManager.Instance.OvertimeToggle = !ApplicationManager.Instance.OvertimeToggle;
         return result;
      }

      public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
      {
         return null;
      }
   }
}
