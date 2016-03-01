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
   /// Logica di interazione per OverlayPanel.xaml
	/// </summary>
	public partial class OverlayPanel : UserControl, IDisposable
	{
      public static readonly DependencyProperty InnerContentProperty = DependencyProperty.Register("InnerContent", typeof(object), typeof(OverlayPanel), new PropertyMetadata(null));
      public static readonly DependencyProperty FixedWidthProperty = DependencyProperty.Register("FixedWidth", typeof(int), typeof(OverlayPanel), new PropertyMetadata(null));
      public static readonly DependencyProperty FixedHeightProperty = DependencyProperty.Register("FixedHeight", typeof(int), typeof(OverlayPanel), new PropertyMetadata(null));

      public event EventHandler PanelCollapsing;

      public object InnerContent
      {
         get { return GetValue(InnerContentProperty); }
         set { SetValue(InnerContentProperty, value); }
      }

      public int FixedWidth
      {
         get { return (int)GetValue(FixedWidthProperty); }
         set { SetValue(FixedWidthProperty, value); }
      }

      public int FixedHeight
      {
         get { return (int)GetValue(FixedHeightProperty); }
         set { SetValue(FixedHeightProperty, value); }
      }

      public OverlayPanel()
      {
         InitializeComponent();
         if (FixedWidth > 0)
         {
            Inner_Grid.Width = FixedWidth;
         }
         if (FixedHeight > 0)
         {
            Inner_Grid.Height = FixedHeight;
         }
      }

      private void Inner_Grid_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
      {
         e.Handled = true;
      }

      private void UserControl_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
      {
         OverlayControl.Visibility = System.Windows.Visibility.Collapsed;
         if (PanelCollapsing != null)
         {
            PanelCollapsing(this, EventArgs.Empty);
         }
      }

      public void Dispose()
      {
      }

      private void OverlayControl_Unloaded(object sender, RoutedEventArgs e)
      {
         this.MouseUp -= UserControl_MouseUp;
         this.Inner_Grid.MouseUp -= Inner_Grid_MouseUp;
      }
   }
}
