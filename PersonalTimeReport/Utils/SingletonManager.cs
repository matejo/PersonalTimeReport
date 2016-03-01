using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalTimeReport
{
   public abstract class SingletonManager<T> : Manager where T : class
   {
      private static System.Lazy<T> m_Instance = null;

      public static T Instance
      {
         get { return m_Instance.Value; }
      }

      static SingletonManager()
      {
         m_Instance = new System.Lazy<T>(() =>
         {
            var constructor = typeof(T).GetConstructor(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic, null, System.Type.EmptyTypes, null);
            return (T)constructor.Invoke(null);
         });
      }
   }

}
