using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PersonalTimeReport
{
   public class SettingsManager : SingletonManager<SettingsManager>, IManager
   {
      #region Members
      private Settings m_Settings;
      private const string SETTINGS_FILE = "Configuration\\Settings.xml";
      #endregion

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

      public Settings Settings
      {
         get
         {
            return m_Settings;
         }
         set
         {
            m_Settings = value;
            this.OnPropertyChanged("Settings");
         }
      }
      #endregion

      #region Methods
      internal void Save()
      {
         try
         {
            Utilities.WriteToXmlFile<Settings>(SETTINGS_FILE, Settings);
         }
         catch { }
      }

      public override void Load()
      {
         try
         {
            Settings = Utilities.ReadFromXmlFile<Settings>(SETTINGS_FILE);
         }
         catch { }
         if (Settings == null)
         {
            Settings = new Settings();
         }
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