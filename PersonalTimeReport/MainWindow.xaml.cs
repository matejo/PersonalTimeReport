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
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         ApplicationManager.Instance.Load();
         InitializeComponent();
         this.Loaded += MainWindow_Loaded;
         this.Unloaded += MainWindow_Unloaded;
      }

      void MainWindow_Unloaded(object sender, RoutedEventArgs e)
      {
         ApplicationManager.Instance.Stop();
      }

      void MainWindow_Loaded(object sender, RoutedEventArgs e)
      {
         ApplicationManager.Instance.Start();
      }
   }
}
