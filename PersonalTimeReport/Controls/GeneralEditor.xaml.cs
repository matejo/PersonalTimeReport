using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
   /// Logica di interazione per GeneralEditor.xaml
	/// </summary>
	public partial class GeneralEditor : UserControl
	{
      public GeneralElements Templates { get; set; }
      public GeneralActions Buttons { get; set; }

      public GeneralEditor(GeneralElements templates, GeneralActions buttons)
      {
         InitializeComponent();
         this.Templates = templates;
         this.Buttons = buttons;
         this.Unloaded += GeneralEditor_Unloaded;
      }

      void GeneralEditor_Unloaded(object sender, RoutedEventArgs e)
      {
         this.Unloaded -= GeneralEditor_Unloaded;
         Templates.Clear();
         Templates = null;
         Buttons.Clear();
         Buttons = null;
         GC.Collect();
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            Button button = sender as Button;
            Action<GeneralElements> action = this.Buttons[string.Concat(button.Tag)];
            if (action != null)
            {
               this.Buttons[string.Concat(button.Tag)].Invoke(this.Templates);
            }
            ApplicationManager.Instance.OverlayPanel.Visibility = System.Windows.Visibility.Collapsed;
            this.ErrorText.Text = string.Empty;
         }
         catch(Exception ex)
         {
            this.ErrorText.Text = ex.Message;
         }
      }
   }
}
