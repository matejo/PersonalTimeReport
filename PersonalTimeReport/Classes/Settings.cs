using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PersonalTimeReport
{
   [Serializable]
   public class Setting
   {
      [XmlAttribute]
      public string Key { get; set; }
      [XmlAttribute]
      public string Value { get; set; }
   }

   [Serializable]
   [XmlRoot("Settings")]
   public class Settings : ObservableCollection<Setting>
   {
      public Setting this[string key]
      {
         get
         {
            return this.FirstOrDefault(w => w.Key == key);
         }
      }
   }
}
