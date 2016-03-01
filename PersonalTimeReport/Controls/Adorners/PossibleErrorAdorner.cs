using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace PersonalTimeReport.Adorners
{
   public class PossibleErrorAdorner : Adorner
   {
      private const double DIMENSION_X = 10;
      private const double DIMENSION_Y = 13;

      private double MARGIN_X = 2;
      private double MARGIN_Y = 2;

      private const double ROUND_CORNER = 1;

      private AdornerLocation m_Location = AdornerLocation.TopLeft;

      public AdornerLocation Location
      {
         get { return m_Location; }
         set { m_Location = value; }
      }

      public PossibleErrorAdorner(UIElement adornedElement) :
         base(adornedElement)
      {
         this.Visibility = System.Windows.Visibility.Hidden;
      }

      protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
      {
         double startX = MARGIN_X;
         double startY = MARGIN_X;

         switch (this.Location)
         {
            case AdornerLocation.TopLeft:
               startX = MARGIN_X;
               startY = MARGIN_Y;
               break;
            case AdornerLocation.TopRight:
               startX = this.ActualWidth - MARGIN_X - DIMENSION_X;
               startY = MARGIN_Y;
               break;
            case AdornerLocation.BottomLeft:
               startX = MARGIN_X;
               startY = this.ActualHeight - MARGIN_Y - DIMENSION_Y;
               break;
            case AdornerLocation.BottomRight:
               startX = this.ActualWidth - MARGIN_X - DIMENSION_X;
               startY = this.ActualHeight - MARGIN_Y - DIMENSION_Y;
               break;
         }

         LinearGradientBrush lockRectangleBrush = new LinearGradientBrush();
         lockRectangleBrush.StartPoint = new Point(0, 0.4);
         lockRectangleBrush.EndPoint = new Point(1, 0.6);
         lockRectangleBrush.GradientStops.Add(new GradientStop(Colors.OrangeRed, 0));
         lockRectangleBrush.GradientStops.Add(new GradientStop(Colors.Red, 0.3));
         lockRectangleBrush.GradientStops.Add(new GradientStop(Colors.Red, 0.6));
         lockRectangleBrush.GradientStops.Add(new GradientStop(Colors.DarkRed, 1));

         Pen lockRectanglePen = new Pen(Brushes.DimGray, 0.5);

         drawingContext.DrawRoundedRectangle(lockRectangleBrush, lockRectanglePen, new Rect(startX + 1, startY, 6, 6), ROUND_CORNER, ROUND_CORNER);
      }

      public static PossibleErrorAdorner Add(UIElement element)
      {
         PossibleErrorAdorner result = null;

         AdornerLayer al = AdornerLayer.GetAdornerLayer(element);
         if (al != null)
         {
            Adorner[] adorners = al.GetAdorners(element);
            if (adorners != null && adorners.Length != 0)
            {
               foreach (Adorner a in adorners)
               {
                  if (a is PossibleErrorAdorner)
                  {
                     result = a as PossibleErrorAdorner;
                     break;
                  }
               }
            }
            if (result == null)
            {
               result = new PossibleErrorAdorner(element);
               al.Add(result);
            }
         }

         return result;
      }

      public static void Remove(UIElement element)
      {
         AdornerLayer al = AdornerLayer.GetAdornerLayer(element);
         if (al != null)
         {
            Adorner[] adorners = al.GetAdorners(element);
            PossibleErrorAdorner toRemove = null;
            if (adorners != null && adorners.Length != 0)
            {
               foreach (Adorner a in adorners)
               {
                  if (a is PossibleErrorAdorner)
                  {
                     toRemove = a as PossibleErrorAdorner;
                     break;
                  }
               }
            }
            if (toRemove != null)
            {
               al.Remove(toRemove);
            }
         }
      }
   }
}