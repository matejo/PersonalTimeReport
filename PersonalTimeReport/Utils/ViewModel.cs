using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace PersonalTimeReport
{
   public class ViewModel : DependencyObject, INotifyPropertyChanged
   {
      private bool m_IsBusy = false;
      private string m_MessageBusy = string.Empty;

      #region INotifyPropertyChanged

      /// <summary>
      /// Occur when a property change its value.
      /// </summary>
      public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChanged(String propertyName)
      {
         PropertyChangedEventHandler handler = PropertyChanged;

         if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
      }

      #endregion

      /// <summary>
      /// Gets or sets the busy state of the manager.
      /// </summary>
      [XmlIgnore]
      public bool IsBusy
      {
         get { return m_IsBusy; }
         set
         {
            m_IsBusy = value;
            this.OnPropertyChanged("IsBusy");
         }
      }

      /// <summary>
      /// Gets or sets the message for the busy state of the manager.
      /// </summary>
      [XmlIgnore]
      public string MessageBusy
      {
         get { return m_MessageBusy; }
         set
         {
            m_MessageBusy = value;
            this.OnPropertyChanged("MessageBusy");
         }
      }
   }
}