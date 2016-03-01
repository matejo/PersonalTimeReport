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
   /// Interaction logic for AccountedHours.xaml
   /// </summary>
   public partial class AccountedHours : UserControl
   {
      public static readonly DependencyProperty AccountTypeProperty = DependencyProperty.Register("AccountType", typeof(string), typeof(AccountedHours));

      public string AccountType
      {
         get { return GetValue(AccountTypeProperty) as string; }
         set { SetValue(AccountTypeProperty, value); }
      }

      public AccountedHours()
      {
         InitializeComponent();
      }
   }
}
