using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PersonalTimeReport
{
   public class TimeTextBox : MaskedTextBox
   {
      public static readonly DependencyProperty CheckTimeBoundariesProperty = DependencyProperty.Register("CheckTimeBoundaries", typeof(bool), typeof(TimeTextBox));
      public static readonly DependencyProperty FreeWidthProperty = DependencyProperty.Register("FreeWidth", typeof(bool), typeof(TimeTextBox));
      public static readonly DependencyProperty IsInputMaskEnabledProperty = DependencyProperty.Register("IsInputMaskEnabled", typeof(bool), typeof(TimeTextBox));

      public bool CheckTimeBoundaries
      {
         get { return (bool)GetValue(CheckTimeBoundariesProperty); }
         set { SetValue(CheckTimeBoundariesProperty, value); }
      }

      public bool FreeWidth
      {
         get { return (bool)GetValue(FreeWidthProperty); }
         set { SetValue(FreeWidthProperty, value); }
      }

      public bool IsInputMaskEnabled
      {
         get { return (bool)GetValue(IsInputMaskEnabledProperty); }
         set { SetValue(IsInputMaskEnabledProperty, value); }
      }

      private Adorners.PossibleErrorAdorner m_PossibleErrorAdorner;

      public TimeTextBox()
         : base()
      {
         this.Loaded += TimeTextBox_Loaded;
      }

      private void TimeTextBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
      {
         if (IsInputMaskEnabled)
         {
            this.Mask = "00:00";
         }
         this.MinWidth = ApplicationManager.Instance.CellWidth;
         if (!FreeWidth)
         {
            this.MaxWidth = this.ActualWidth;
         }
         m_PossibleErrorAdorner = Adorners.PossibleErrorAdorner.Add(this);
      }

      protected override void OnTextChanged(TextChangedEventArgs e)
      {
         if (CheckTimeBoundaries)
         {
            updatePossibleErrorAdorner();
         }
         base.OnTextChanged(e);
      }

      private void updatePossibleErrorAdorner()
      {
         if (!string.IsNullOrEmpty(this.Text) && m_PossibleErrorAdorner != null)
         {
            TimeSpan ts = TimeSpan.Zero;
            if (TimeSpan.TryParse(this.Text, out ts))
            {
               if (ts != TimeSpan.Zero)
               {
                  if (ts < TimeSpan.Parse("07:00:00") || ts > TimeSpan.Parse("20:00:00"))
                  {
                     m_PossibleErrorAdorner.Visibility = System.Windows.Visibility.Visible;
                  }
                  else
                  {
                     m_PossibleErrorAdorner.Visibility = System.Windows.Visibility.Collapsed;
                  }
               }
               else
               {
                  m_PossibleErrorAdorner.Visibility = System.Windows.Visibility.Collapsed;
               }
            }
         }
      }
   }
}