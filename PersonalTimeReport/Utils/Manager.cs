using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalTimeReport
{
   public abstract class Manager : ViewModel
   {
      public abstract void Load();
      public abstract void Start();
      public abstract void Stop();
   }
}
