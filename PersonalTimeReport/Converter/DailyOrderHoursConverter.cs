using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace PersonalTimeReport
{
   public class DailyOrderHoursConverter : IMultiValueConverter
   {
      public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
      {
         WorkingMonth wm = values[0] as WorkingMonth;
         string id = values[1] as string;
         string type = parameter as string;
         TimeSpan result = TimeSpan.Zero;
         if (type.ToLower() == "real")
         {
            wm.ToList().ForEach(mon => mon.WorkDay.Where(wd => wd.Key.Id == id).ToList().ForEach(wd => result += wd.Value.RealHours));
         }
         else if (type.ToLower() == "delivered")
         {
            wm.ToList().ForEach(mon => mon.WorkDay.Where(wd => wd.Key.Id == id).ToList().ForEach(wd => result += wd.Value.DeliveredHours));
         }
         else if (type.ToLower() == "overtime")
         {
            wm.ToList().ForEach(mon => 
               //result += (wd.Value.DeliveredHours > ApplicationManager.Instance.HOUR_TOTAL) ? wd.Value.DeliveredHours - ApplicationManager.Instance.HOUR_TOTAL : TimeSpan.Zero)
               result += Utilities.CalculateOvertime(mon.MorningEntrance,mon.MorningExit,mon.EveningEntrance, mon.EveningExit)
               );
         }
         return result;
      }

      public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
      {
         return null;
      }
   }
}
