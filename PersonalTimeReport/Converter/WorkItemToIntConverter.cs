using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace PersonalTimeReport
{
   public class WorkItemToTimeSpanConverter : IMultiValueConverter
   {
      public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
      {
         WorkDay WorkDay = value[0] as WorkDay;
         string orderId = value[1] as string;
         string selector = parameter as string;
         TimeSpan result = TimeSpan.Zero;
         if (WorkDay.Count(kvp => kvp.Key.Id == orderId) == 0)
         {
            OrdersManager OrdersManager = (ApplicationManager.Instance.Managers["OrdersManager"] as OrdersManager);
            Order order = OrdersManager.Orders.FirstOrDefault(or => or.Id == orderId);
            if (order != null)
            {
               WorkDay.Add(order, new WorkItem() { DeliveredHours = TimeSpan.Zero, RealHours = TimeSpan.Zero });
            }
            else
            {
               return result;
            }
         }
         WorkItem wi = WorkDay[orderId];
         if (selector.ToLower() == "real")
         {
            result = wi.RealHours;
         }
         else
         {
            result = wi.DeliveredHours;
         }
         return result;
      }

      public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
      {
         string selector = parameter as string;
         TimeSpan temp = TimeSpan.Parse(string.Concat(value));
         WorkDay WorkDay = ApplicationManager.Instance.CurrentTimeTextBoxData.Item1;
         string OrderId = ApplicationManager.Instance.CurrentTimeTextBoxData.Item2;
         if (!string.IsNullOrEmpty(selector))
         {
            if (selector.ToLower() == "real")
            {
               WorkDay.FirstOrDefault(kvp => kvp.Key.Id == OrderId).Value.RealHours = temp;
            }
            else
            {
               WorkDay.FirstOrDefault(kvp => kvp.Key.Id == OrderId).Value.DeliveredHours = temp;
            }
         }
         return new object[] { WorkDay, OrderId };
      }
   }
}
