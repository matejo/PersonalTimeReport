using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalTimeReport
{
   public enum ManagerPriority
   {
      Low,
      Medium,
      High,
      Maximum,
      EntryPoint
   }

   public interface IManager
   {
      ManagerPriority Priority { get; }
      string FullName { get; }

      void Load();
      void Start();
      void Stop();
   }
}
