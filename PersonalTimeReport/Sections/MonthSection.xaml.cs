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
   /// Interaction logic for MonthSection.xaml
   /// </summary>
   public partial class MonthSection : UserControl
   {
      public MonthSection()
      {
         InitializeComponent();
      }

      private void Image_MouseUp(object sender, MouseButtonEventArgs e)
      {
         ApplicationManager.Instance.AdditionalTimeShow = !ApplicationManager.Instance.AdditionalTimeShow;
      }
   }
}
