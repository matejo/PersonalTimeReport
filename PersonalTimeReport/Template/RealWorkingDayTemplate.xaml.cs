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
   /// Interaction logic for RealWorkingDayTemplate.xaml
   /// </summary>
   public partial class RealWorkingDayTemplate : UserControl
   {
      public RealWorkingDayTemplate()
      {
         InitializeComponent();
      }

      private void TimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
      {
         ApplicationManager.Instance.SaveCurrentTimeTextBoxData(sender, this.DataContext);
      }

      private void LabelExt_MouseUp(object sender, MouseButtonEventArgs e)
      {
         (ApplicationManager.Instance.Managers["CommandsManager"] as CommandsManager).Order_CRUD.Execute(this.DataContext);
      }
   }
}
