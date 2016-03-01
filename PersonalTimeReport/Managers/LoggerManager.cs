using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PersonalTimeReport
{
   public enum LogLevel
   {
      Debug,
      Info,
      Warn,
      Error
   }

   public class LoggerManager : SingletonManager<LoggerManager>, IManager
   {
      #region Properties
      public ManagerPriority Priority
      {
         get
         {
            return ManagerPriority.Maximum;
         }
      }

      public string FullName
      {
         get
         {
            return this.GetType().FullName;
         }
      }

      public LogLevel LogLevel { get; set; }
      #endregion

      #region Methods
      private void Log(LogLevel logLevel, string message)
      {
         if (logLevel >= this.LogLevel)
         {
            //logga message
         }
      }

      public void LogDebug(string message) { Log(PersonalTimeReport.LogLevel.Debug, message); }
      public void LogError(string message) { Log(PersonalTimeReport.LogLevel.Error, message); }
      public void LogWarn(string message) { Log(PersonalTimeReport.LogLevel.Warn, message); }
      public void LogInfo(string message) { Log(PersonalTimeReport.LogLevel.Info, message); }

      public override void Load()
      {
      }

      public override void Start()
      {
      }

      public override void Stop()
      {
      }
      #endregion
   }
}