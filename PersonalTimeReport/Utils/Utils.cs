using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PersonalTimeReport
{
   public class Utilities
   {
      public static T DeserializeObject<T>(string toDeserialize)
      {
         T resultingMessage = default(T);
         System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
         using (System.IO.StringReader rdr = new System.IO.StringReader(toDeserialize))
         {
            resultingMessage = (T)serializer.Deserialize(rdr);
         }
         return resultingMessage;
      }

      public static string SerializeObject<T>(T toSerialize)
      {
         System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(toSerialize.GetType());

         using (System.IO.StringWriter textWriter = new System.IO.StringWriter())
         {
            xmlSerializer.Serialize(textWriter, toSerialize);
            return textWriter.ToString();
         }
      }

      public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
      {
         TextWriter writer = null;
         try
         {
            var serializer = new XmlSerializer(typeof(T));
            writer = new StreamWriter(filePath, append);
            serializer.Serialize(writer, objectToWrite);
         }
         finally
         {
            if (writer != null)
               writer.Close();
         }
      }

      public static T ReadFromXmlFile<T>(string filePath) where T : new()
      {
         TextReader reader = null;
         try
         {
            var serializer = new XmlSerializer(typeof(T));
            reader = new StreamReader(filePath);
            return (T)serializer.Deserialize(reader);
         }
         finally
         {
            if (reader != null)
               reader.Close();
         }
      }

      public static ManagerDictionary CreateManagers(System.Reflection.Assembly assembly)
      {
         ManagerDictionary Managers = new ManagerDictionary();
         foreach (Type type in assembly.GetTypes().Where(ty => !ty.IsInterface && typeof(IManager).IsAssignableFrom(ty)))
         {
            Managers.Add(type.Name, (IManager)Activator.CreateInstance(type));
         }
         return Managers;
      }

      public static void CallManagers(ManagerDictionary Managers, string Method)
      {
         foreach (var manager in Managers.Values.OrderByDescending(or => or.Priority))
         {
            manager.GetType().GetMethod(Method).Invoke(manager, null);
         }
      }

      public static int GetWeekNumer(DateTime date)
      {
         return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
      }

      public static TimeSpan CalculateOvertime(TimeSpan? MorningEntrance, TimeSpan? MorningExit, TimeSpan? EveningEntrance, TimeSpan? EveningExit)
      {
         TimeSpan result = TimeSpan.Zero;
         if (MorningEntrance.HasValue && MorningExit.HasValue && EveningEntrance.HasValue && EveningExit.HasValue)
         {
            TimeSpan temp = (EveningExit.Value - EveningEntrance.Value) + (MorningExit.Value - MorningEntrance.Value);
            if (temp > ApplicationManager.Instance.HOUR_TOTAL)
            {
               result = temp - ApplicationManager.Instance.HOUR_TOTAL;
            }
         }
         return result;
      }
   }
}
