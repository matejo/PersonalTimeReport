using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace PersonalTimeReport
{
   public class Ticker : INotifyPropertyChanged
   {
      #region Event
      public event PropertyChangedEventHandler PropertyChanged;
      #endregion

      #region Members
      DispatcherTimer m_Timer;
      #endregion

      #region Properties
      public string Now
      {
         get
         {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
         }
      }

      public bool IsWithPropertyChanged { get; set; }
      #endregion

      #region CTOR
      public Ticker()
      {
         m_Timer = new DispatcherTimer();
         IsWithPropertyChanged = true;
         Start();
      }

      public Ticker(int seconds)
      {
         m_Timer = new DispatcherTimer();
         IsWithPropertyChanged = true;
         Start(seconds);
      }
      #endregion

      #region Methods
      public void Start(int delay = 1)
      {
         m_Timer.Interval = TimeSpan.FromSeconds(delay);
         m_Timer.Tick += timer_Tick;
         m_Timer.Start();
      }

      public void Stop()
      {
         m_Timer.Stop();
         m_Timer.Tick -= timer_Tick;
      }

      private void timer_Tick(object sender, object e)
      {
         if (PropertyChanged != null && IsWithPropertyChanged)
            PropertyChanged(this, new PropertyChangedEventArgs("Now"));
      }
      #endregion
   }
}
