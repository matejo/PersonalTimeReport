using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PersonalTimeReport
{
   public class ApplicationManager : SingletonManager<ApplicationManager>
   {
      #region Members
      private bool m_OvertimeToggle;

      private int m_AccountedVisibleWidth;
      private bool m_AdditionalTimeShow;
      private string m_Title;
      private OverlayPanel m_OverlayPanel;
      #endregion

      #region Properties
      public ManagerDictionary Managers { get; set; }

      public string Title
      {
         get
         {
            return m_Title;
         }
         set
         {
            m_Title = value;
            this.OnPropertyChanged("Title");
         }
      }
      public double MaxWidth { get; set; }
      public double MaxHeight { get; set; }
      public int AccountedVisibleWidth
      {
         get
         {
            return m_AccountedVisibleWidth;
         }
         set
         {
            m_AccountedVisibleWidth = value;
            this.OnPropertyChanged("AccountedVisibleWidth");
         }
      }
      public int CellWidth { get; set; }
      public bool AdditionalTimeShow
      {
         get
         {
            return m_AdditionalTimeShow;
         }
         set
         {
            m_AdditionalTimeShow = value;
            this.OnPropertyChanged("AdditionalTimeShow");
         }
      }

      public ObservableCollection<string> VisibleWeeks { get; set; }

      public bool OvertimeToggle
      {
         get
         {
            return m_OvertimeToggle;
         }
         set
         {
            m_OvertimeToggle = value;
            this.OnPropertyChanged("OvertimeToggle");
         }
      }
      public TimeSpan HOUR_TOTAL { get; set; }
      public Tuple<WorkDay, string> CurrentTimeTextBoxData { get; set; }
      public OverlayPanel OverlayPanel
      {
         get
         {
            return m_OverlayPanel;
         }
         set
         {
            m_OverlayPanel = value;
            this.OnPropertyChanged("OverlayPanel");
         }
      }
      #endregion

      #region Methods
      internal void SaveCurrentTimeTextBoxData(object sender, object DataContext)
      {
         if (sender is TimeTextBox && DataContext is Order)
         {
            CurrentTimeTextBoxData = new Tuple<WorkDay, string>(((sender as TimeTextBox).DataContext as WorkingDay).WorkDay, (DataContext as Order).Id);
         }
         else
         {
            CurrentTimeTextBoxData = null;
         }
      }

      public override void Load()
      {
         VisibleWeeks = new ObservableCollection<string>();
         VisibleWeeks.CollectionChanged += (sender, e) =>
         {
            this.OnPropertyChanged("VisibleWeeks");
         };
         Managers = new ManagerDictionary();
         Managers = Utilities.CreateManagers(this.GetType().Assembly);
         Utilities.CallManagers(Managers, System.Reflection.MethodInfo.GetCurrentMethod().Name);
         Settings settings = (Managers["SettingsManager"] as SettingsManager).Settings;
         this.CellWidth = int.Parse(settings["CellWidth"].Value);
         this.AccountedVisibleWidth = int.Parse(settings["AccountedVisibleWidth"].Value);
         this.HOUR_TOTAL = new TimeSpan(int.Parse(settings["HOUR_TOTAL"].Value), 0, 0);
         this.MaxWidth = double.Parse(settings["MaxWidth"].Value);
         this.MaxHeight = double.Parse(settings["MaxHeight"].Value);
         SetWindowTitle();
      }

      internal void SetWindowTitle(string name = null)
      {
         if (!string.IsNullOrEmpty(name))
         {
            this.Title = string.Format("{0} - ({1})", "Personal Time Report", name);
         }
         else
         {
            this.Title = "Personal Time Report";
         }
      }

      public override void Start()
      {
         Utilities.CallManagers(Managers, System.Reflection.MethodInfo.GetCurrentMethod().Name);
      }

      public override void Stop()
      {
         Utilities.CallManagers(Managers, System.Reflection.MethodInfo.GetCurrentMethod().Name);
      }
      #endregion
   }
}