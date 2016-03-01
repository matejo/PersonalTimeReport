using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalTimeReport
{
   public class ManagerDictionary : DictionaryViewModel<string, IManager>
   {
      public new object this[string manager]
      {
         get
         {
            return this.FirstOrDefault(w => w.Key == manager).Value;
         }
      }
   }
}
