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
   /// Interaction logic for AccountedHoursTemplate.xaml
   /// </summary>
   public partial class AccountedHoursTemplate : UserControl
   {
      public static readonly DependencyProperty TemplateAccountTypeProperty = DependencyProperty.Register("TemplateAccountType", typeof(string), typeof(AccountedHoursTemplate));

      public string TemplateAccountType
      {
         get { return GetValue(TemplateAccountTypeProperty) as string; }
         set { SetValue(TemplateAccountTypeProperty, value); }
      }

      public AccountedHoursTemplate()
      {
         InitializeComponent();
         this.Loaded += AccountedHoursTemplate_Loaded;
      }

      void AccountedHoursTemplate_Loaded(object sender, RoutedEventArgs e)
      {
         if (!string.IsNullOrEmpty(TemplateAccountType))
         {
            if (TemplateAccountType.ToLower() == "real")
            {
               this.AccountHoursTemplateName.Content = new RealWorkingDayTemplate();
               //AccountHoursTemplateName.DataContext = this.DataContext;
            }
            else
            {
               this.AccountHoursTemplateName.Content = new DeliveredWorkingDayTemplate();
               //AccountHoursTemplateName.DataContext = this.DataContext;
            }
         }
      }
   }
}
