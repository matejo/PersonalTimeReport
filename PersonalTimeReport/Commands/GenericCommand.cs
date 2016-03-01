using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PersonalTimeReport
{
   public class GenericCommand : ICommand
   {
      private Action<object> m_Action;
      private bool m_CanExecute;

      public GenericCommand(Action<object> action, bool canExecute)
      {
         m_Action = action;
         m_CanExecute = canExecute;
      }

      public bool CanExecute(object parameter)
      {
         return m_CanExecute;
      }

      public event EventHandler CanExecuteChanged;

      public void Execute(object parameter)
      {
         m_Action(parameter);
      }
   }
}