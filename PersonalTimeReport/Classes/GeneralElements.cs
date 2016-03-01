using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace PersonalTimeReport
{
   public class GeneralElements : DictionaryViewModel<string, object>
   {
   }

   public class GeneralActions : DictionaryViewModel<string, Action<GeneralElements>>
   {
   }
}
