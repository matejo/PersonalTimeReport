using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PersonalTimeReport
{
   public class CommandsManager : SingletonManager<CommandsManager>, IManager
   {
      #region Members
      private GenericCommand m_NewCalendarCommand;
      private GenericCommand m_LoadCalendarCommand;
      private GenericCommand m_SaveCalendarCommand;
      private GenericCommand m_NextMonthCommand;
      private GenericCommand m_PrevMonthCommand;
      private GenericCommand m_Order_CRUDCommand;
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

      public GenericCommand NewCalendar
      {
         get
         {
            if (m_NewCalendarCommand == null)
            {
               m_NewCalendarCommand = new GenericCommand(new Action<object>((parameter) =>
               {
                  if (System.Windows.MessageBox.Show("Procedo con un nuovo calendario?", "Warning", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                  {
                     (ApplicationManager.Instance.Managers["CalendarsManager"] as CalendarsManager).LastSaveFile = string.Empty;
                     (ApplicationManager.Instance.Managers["CalendarsManager"] as CalendarsManager).NewCalendar(DateTime.Now.Year);
                  }
               }), true);
            }
            return m_NewCalendarCommand;
         }
      }
      public GenericCommand SaveCalendar
      {
         get
         {
            if (m_SaveCalendarCommand == null)
            {
               m_SaveCalendarCommand = new GenericCommand(new Action<object>((parameter) =>
               {
                  string filename = (ApplicationManager.Instance.Managers["CalendarsManager"] as CalendarsManager).LastSaveFile;
                  if (string.IsNullOrEmpty(filename))
                  {
                     Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                     saveFileDialog.Filter = "PTR files (*.ptrf)|*.ptrf";
                     saveFileDialog.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                     if (saveFileDialog.ShowDialog() == true)
                     {
                        (ApplicationManager.Instance.Managers["CalendarsManager"] as CalendarsManager).SaveCalendar(saveFileDialog.FileName);
                     }
                  }
                  else
                  {
                     (ApplicationManager.Instance.Managers["CalendarsManager"] as CalendarsManager).SaveCalendar(filename);
                  }
               }), true);
            }
            return m_SaveCalendarCommand;
         }
      }
      public GenericCommand LoadCalendar
      {
         get
         {
            if (m_LoadCalendarCommand == null)
            {
               m_LoadCalendarCommand = new GenericCommand(new Action<object>((parameter) =>
               {
                  Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                  openFileDialog.Filter = "PTR files (*.ptrf)|*.ptrf|All files (*.*)|*.*";
                  openFileDialog.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                  if (openFileDialog.ShowDialog() == true)
                  {
                     (ApplicationManager.Instance.Managers["CalendarsManager"] as CalendarsManager).LoadCalendar(openFileDialog.FileName);
                  }
               }), true);
            }
            return m_LoadCalendarCommand;
         }
      }
      public GenericCommand NextMonth
      {
         get
         {
            if (m_NextMonthCommand == null)
            {
               m_NextMonthCommand = new GenericCommand(new Action<object>((parameter) => { (ApplicationManager.Instance.Managers["CalendarsManager"] as CalendarsManager).ChangeMonth(1); }), true);
            }
            return m_NextMonthCommand;
         }
      }
      public GenericCommand PrevMonth
      {
         get
         {
            if (m_PrevMonthCommand == null)
            {
               m_PrevMonthCommand = new GenericCommand(new Action<object>((parameter) => { (ApplicationManager.Instance.Managers["CalendarsManager"] as CalendarsManager).ChangeMonth(-1); }), true);
            }
            return m_PrevMonthCommand;
         }
      }
      public GenericCommand Order_CRUD
      {
         get
         {
            if (m_Order_CRUDCommand == null)
            {
               m_Order_CRUDCommand = new GenericCommand(new Action<object>((parameter) =>
               {
                  Order editableOrder = parameter as Order;
                  GeneralElements templates = new GeneralElements();
                  GeneralActions buttons = new GeneralActions();
                  if (editableOrder == null)
                  {
                     templates.Add("Id", new System.Windows.Controls.TextBox());
                     templates.Add("Name", new System.Windows.Controls.TextBox());
                  }
                  else
                  {
                     templates.Add("Id", new System.Windows.Controls.TextBox() { Text = editableOrder.Id });
                     templates.Add("Name", new System.Windows.Controls.TextBox() { Text = editableOrder.Name });
                  }
                  buttons.Add("Salva", (ge) =>
                  {
                     (ApplicationManager.Instance.Managers["OrdersManager"] as OrdersManager).AddOrUpdate((ge["Id"] as System.Windows.Controls.TextBox).Text, (ge["Name"] as System.Windows.Controls.TextBox).Text);
                     (ApplicationManager.Instance.Managers["NotificationManager"] as NotificationManager).Notify("Ordine Creato");
                  });
                  if (editableOrder != null)
                  {
                     buttons.Add("Cancella", (ge) =>
                     {
                        (ApplicationManager.Instance.Managers["OrdersManager"] as OrdersManager).Delete((ge["Id"] as System.Windows.Controls.TextBox).Text);
                        (ApplicationManager.Instance.Managers["NotificationManager"] as NotificationManager).Notify("Ordine Cancellato");
                     });
                  }
                  buttons.Add("Annulla", null);
                  ApplicationManager.Instance.OverlayPanel = new OverlayPanel()
                  {
                     InnerContent = new GeneralEditor(templates, buttons),
                     Visibility = System.Windows.Visibility.Visible
                  };
               }), true);
            }
            return m_Order_CRUDCommand;
         }
      }
      #endregion

      #region Methods
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