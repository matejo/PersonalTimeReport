using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace PersonalTimeReport
{
   public class OrderToDescriptionConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         Order order = value as Order;
         string result = string.Empty;
         if (order != null)
         {
            int tryparse = int.MaxValue;
            result = string.Format("{0} ({1})", order.Name, order.Id);
            if (int.TryParse(order.Id, out tryparse))
            {
               if (tryparse < 0)
               {
                  result = string.Format("{0}", order.Name);
               }
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
