using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonalTimeReport
{
   /// <summary>
   /// Interaction logic for WorkingDayTemplate.xaml
   /// </summary>
   public partial class WorkingDayTemplate : UserControl
   {
      public WorkingDayTemplate()
      {
         InitializeComponent();
      }

      private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
      {
         WorkingDay wd = this.DataContext as WorkingDay;
         string id = string.Concat(wd.ActualWorkingDay.Year.ToString(), wd.WeekNumber.ToString().PadLeft(2,'0'));
         if (ApplicationManager.Instance.VisibleWeeks.Contains(id))
         {
            ApplicationManager.Instance.VisibleWeeks.Remove(id);
         }
         else
         {
            ApplicationManager.Instance.VisibleWeeks.Add(id);
         }
      }
   }
}
