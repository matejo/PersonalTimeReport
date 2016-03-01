using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace PersonalTimeReport
{
   public class CalendarsManager : SingletonManager<CalendarsManager>, IManager
   {
      #region Members
      private WorkingMonth m_CurrentMonth;
      private WorkingYear m_CurrentYear;
      private WorkingLife m_WorkingLife;
      private BackgroundWorker m_OperationWorker;
      private AutoResetEvent WaitForFinish = new AutoResetEvent(false);
      #endregion

      #region Properties
      public ManagerPriority Priority
      {
         get
         {
            return ManagerPriority.Low;
         }
      }

      public string FullName
      {
         get
         {
            return this.GetType().FullName;
         }
      }

      public WorkingYear CurrentYear
      {
         get
         {
            return m_CurrentYear;
         }
         set
         {
            m_CurrentYear = value;
            this.OnPropertyChanged("CurrentYear");
            this.OnPropertyChanged("CurrentMonth");
         }
      }
      public WorkingMonth CurrentMonth
      {
         get
         {
            return m_CurrentMonth;
         }
         set
         {
            m_CurrentMonth = value;
            this.OnPropertyChanged("CurrentMonth");
         }
      }
      public WorkingLife WorkingLife
      {
         get
         {
            return m_WorkingLife;
         }
         set
         {
            m_WorkingLife = value;
            this.OnPropertyChanged("WorkingLife");
         }
      }

      public string LastSaveFile { get; set; }
      #endregion

      #region Methods
      void OperationWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         ApplicationManager.Instance.IsBusy = false;
      }

      void OperationWorker_DoWork(object sender, DoWorkEventArgs e)
      {
         ApplicationManager.Instance.IsBusy = true;
         Tuple<string, object> argument = e.Argument as Tuple<string, object>;
         switch (argument.Item1)
         {
            case "New":
               //Dispatcher.Invoke(new Action(() =>
               //{
               //   if (WorkingLife != null)
               //   {
               //      WorkingLife.Clear();
               //   }
               //   if (CurrentYear != null)
               //   {
               //      CurrentYear.Clear();
               //   }
               //   if (CurrentMonth != null)
               //   {
               //      CurrentMonth.Clear();
               //   }
               //}));
               int thisYear = (int)argument.Item2;
               WorkingYear year = new WorkingYear() { Year = thisYear };
               for (int month = 1; month <= 12; month++)
               {
                  DateTime FirstOfMonth = new DateTime(thisYear, month, 1);
                  DateTime LastOfMonth = new DateTime(thisYear, month, DateTime.DaysInMonth(thisYear, month));
                  WorkingMonth tMonth = new WorkingMonth(month);
                  for (int day = 1; day <= LastOfMonth.Day; day++)
                  {
                     DateTime wddt = new DateTime(thisYear, month, day);
                     WorkingDay wd = new WorkingDay(wddt);
                     tMonth.Add(wd);
                  }
                  year.Add(tMonth);
               }
               CurrentMonth = year[System.DateTime.Now.Month - 1];
               CurrentYear = year;
               WorkingLife.Clear();
               WorkingLife.Add(CurrentYear.Year, CurrentYear);
               WaitForFinish.Set();
               break;
            case "Save":
               Utilities.WriteToXmlFile<WorkingLife>(string.Concat(argument.Item2), WorkingLife);
               this.LastSaveFile = string.Concat(argument.Item2);
               break;
            case "Load":
               WorkingLife.Clear();
               WorkingLife = Utilities.ReadFromXmlFile<WorkingLife>(string.Concat(argument.Item2));
               CurrentYear = WorkingLife.LastOrDefault().Value;
               CurrentYear.Year = WorkingLife.LastOrDefault().Key;
               CurrentMonth = CurrentYear[DateTime.Now.Month - 1];
               this.LastSaveFile = string.Concat(argument.Item2);
               break;
         }
      }

      internal void SaveCalendar(string FilePath = null)
      {
         m_OperationWorker.RunWorkerAsync(new Tuple<string, object>("Save", FilePath));
         ApplicationManager.Instance.SetWindowTitle(Path.GetFileName(FilePath));
      }

      internal void LoadCalendar(string FilePath = null)
      {
         m_OperationWorker.RunWorkerAsync(new Tuple<string, object>("Load", FilePath));
         ApplicationManager.Instance.SetWindowTitle(Path.GetFileName(FilePath));
      }

      internal void NewCalendar(int Year)
      {
         m_OperationWorker.RunWorkerAsync(new Tuple<string, object>("New", Year));
         ApplicationManager.Instance.SetWindowTitle();
      }

      internal void ChangeMonth(int shift)
      {
         if (shift < 0)
         {
            if (this.CurrentMonth.MonthId > 1)
            {
               this.CurrentMonth = CurrentYear[this.CurrentMonth.MonthId - 1 + shift];
            }
            else
            {
               ChangeYear(-1);
               this.CurrentMonth = CurrentYear[12 + shift];
            }
         }
         else if (shift > 0)
         {
            if (this.CurrentMonth.MonthId < 12)
            {
               this.CurrentMonth = CurrentYear[this.CurrentMonth.MonthId - 1 + shift];
            }
            else
            {
               ChangeYear(1);
               this.CurrentMonth = CurrentYear[shift - 1];
            }
         }
      }

      private void ChangeYear(int shift)
      {
         ApplicationManager.Instance.IsBusy = true;
         if (!WorkingLife.ContainsKey(CurrentYear.Year + shift))
         {
            NewCalendar(CurrentYear.Year + shift);
            WaitForFinish.WaitOne();
         }
         else
         {
            this.CurrentYear = WorkingLife[CurrentYear.Year + shift];
         }
         if (shift > 0)
         {
            this.CurrentMonth = this.CurrentYear.FirstOrDefault();
         }
         else
         {
            this.CurrentMonth = this.CurrentYear.LastOrDefault();
         }
         ApplicationManager.Instance.IsBusy = false;
      }

      public override void Load()
      {
         WorkingLife = new WorkingLife();

         m_OperationWorker = new BackgroundWorker();
         m_OperationWorker.DoWork += OperationWorker_DoWork;
         m_OperationWorker.RunWorkerCompleted += OperationWorker_RunWorkerCompleted;
         NewCalendar(DateTime.Now.Year);
         WaitForFinish.WaitOne();
         this.CurrentMonth = this.CurrentYear[DateTime.Now.Month - 1];
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