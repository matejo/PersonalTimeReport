using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace PersonalTimeReport
{
   public class NotificationMessage
   {
      #region Members
      private string m_Message;
      private DateTime m_Timestamp;
      #endregion

      #region Properties
      public string Message
      {
         get { return m_Message; }
         set { m_Message = value; }
      }

      public DateTime Timestamp
      {
         get { return m_Timestamp; }
         set { m_Timestamp = value; }
      }
      #endregion
   }

   public class NotificationManager : SingletonManager<NotificationManager>, IManager
   {
      private const double NOTIFY_DELAY = 2.5;
      private const double TIMER_INTERVAL = 500;

      #region Members
      private ObservableCollection<NotificationMessage> m_Messages;
      private Timer m_Timer;
      private static object m_Sync = new object();
      #endregion

      #region Properties
      public ManagerPriority Priority
      {
         get
         {
            return ManagerPriority.High;
         }
      }

      public string FullName
      {
         get
         {
            return this.GetType().FullName;
         }
      }

      public ObservableCollection<NotificationMessage> Messages
      {
         get
         {
            return m_Messages;
         }
         set
         {
            m_Messages = value;
            this.OnPropertyChanged("Messages");
         }
      }
      #endregion

      #region Methods
      public void Notify(string message)
      {
         lock (m_Sync)
         {
            this.Messages.Add(new NotificationMessage() { Message = message, Timestamp = DateTime.Now });
         }
      }

      void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
      {
         m_Timer.Stop();
         lock (m_Sync)
         {
            NotificationMessage message = this.Messages.FirstOrDefault(me => me.Timestamp.AddSeconds(NOTIFY_DELAY) <= DateTime.Now);
            if (message != null)
            {
               Dispatcher.Invoke(new Action(() =>
               {
                  this.Messages.Remove(message);
               }));
            }
         }
         m_Timer.Start();
      }

      public override void Load()
      {
         this.Messages = new ObservableCollection<NotificationMessage>();
      }

      public override void Start()
      {
         m_Timer = new Timer();
         m_Timer.Interval = TIMER_INTERVAL;
         m_Timer.Elapsed += timer_Elapsed;
         m_Timer.Enabled = true;
         m_Timer.Start();
      }

      public override void Stop()
      {
         m_Timer.Elapsed -= timer_Elapsed;
         m_Timer.Enabled = false;
         m_Timer.Stop();
         m_Timer.Dispose();
         m_Timer = null;
      }
      #endregion
   }
}