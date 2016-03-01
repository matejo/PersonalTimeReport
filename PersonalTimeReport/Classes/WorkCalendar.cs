using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

namespace PersonalTimeReport
{
   [Serializable]
   public class WorkDay : DictionaryViewModel<Order, WorkItem>
   {
      #region Operations
      public WorkItem this[string orderId]
      {
         get
         {
            return this.FirstOrDefault(w => w.Key.Id == orderId).Value;
         }
      }
      #endregion
   }

   [Serializable]
   public class WorkingDay : ViewModel, IXmlSerializable
   {
      #region Members
      private DateTime m_ActualWorkingDay;
      private TimeSpan m_MorningEntrance;
      private TimeSpan m_MorningExit;
      private TimeSpan m_EveningEntrance;
      private TimeSpan m_EveningExit;
      private WorkDay m_WorkDay;
      #endregion

      #region Properties
      public DateTime ActualWorkingDay
      {
         get
         {
            return m_ActualWorkingDay;
         }
         set
         {
            m_ActualWorkingDay = value;
            this.OnPropertyChanged("ActualWorkingDay");
         }
      }
      public TimeSpan MorningEntrance
      {
         get
         {
            return m_MorningEntrance;
         }
         set
         {
            m_MorningEntrance = value;
            this.OnPropertyChanged("MorningEntrance");
         }
      }
      public TimeSpan MorningExit
      {
         get
         {
            return m_MorningExit;
         }
         set
         {
            m_MorningExit = value;
            this.OnPropertyChanged("MorningExit");
         }
      }
      public TimeSpan EveningEntrance
      {
         get
         {
            return m_EveningEntrance;
         }
         set
         {
            m_EveningEntrance = value;
            this.OnPropertyChanged("EveningEntrance");
         }
      }
      public TimeSpan EveningExit
      {
         get
         {
            return m_EveningExit;
         }
         set
         {
            m_EveningExit = value;
            this.OnPropertyChanged("EveningExit");
         }
      }

      public WorkDay WorkDay
      {
         get { return m_WorkDay; }
         set { m_WorkDay = value; }
      }

      #region Calculated Properties
      [XmlIgnore]
      public string DayOfTheWeek
      {
         get
         {
            return ActualWorkingDay.DayOfWeek.ToString().Substring(0, 3);
         }
      }
      [XmlIgnore]
      public int DayNumber
      {
         get
         {
            return ActualWorkingDay.Day;
         }
      }
      [XmlIgnore]
      public int WeekNumber
      {
         get
         {
            return Utilities.GetWeekNumer(ActualWorkingDay);
         }
      }
      #endregion

      #endregion

      #region CTOR
      public WorkingDay()
      {
      }

      public WorkingDay(DateTime dateTime)
      {
         ActualWorkingDay = dateTime;
         m_WorkDay = new WorkDay();
      }
      #endregion

      #region IXmlSerializable
      public System.Xml.Schema.XmlSchema GetSchema()
      {
         throw null;
      }

      public void ReadXml(System.Xml.XmlReader reader)
      {
         XmlSerializer serializerWD = new XmlSerializer(typeof(WorkDay));

         if (reader.IsEmptyElement)
            return;

         this.ActualWorkingDay = DateTime.Parse(reader.GetAttribute("ActualWorkingDay"));
         this.MorningEntrance = TimeSpan.Parse(reader.GetAttribute("MorningEntrance"));
         this.MorningExit = TimeSpan.Parse(reader.GetAttribute("MorningExit"));
         this.EveningEntrance = TimeSpan.Parse(reader.GetAttribute("EveningEntrance"));
         this.EveningExit = TimeSpan.Parse(reader.GetAttribute("EveningExit"));
         
         reader.Read();

         while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
         {
            this.WorkDay = (WorkDay)serializerWD.Deserialize(reader);

            reader.MoveToContent();
         }
         reader.ReadEndElement();
      }

      public void WriteXml(System.Xml.XmlWriter writer)
      {
         XmlSerializer serializerWD = new XmlSerializer(typeof(WorkDay));

         writer.WriteAttributeString("ActualWorkingDay", this.ActualWorkingDay.ToString());
         writer.WriteAttributeString("MorningEntrance", this.MorningEntrance.ToString());
         writer.WriteAttributeString("MorningExit", this.MorningExit.ToString());
         writer.WriteAttributeString("EveningEntrance", this.EveningEntrance.ToString());
         writer.WriteAttributeString("EveningExit", this.EveningExit.ToString());

         serializerWD.Serialize(writer, this.WorkDay);
      }
      #endregion
   }

   [Serializable]
   public class WorkingMonth : ObservableCollection<WorkingDay>, IXmlSerializable
   {
      #region Operations
      public new WorkingDay this[int id]
      {
         get
         {
            return this.FirstOrDefault(w => w.DayNumber == id);
         }
      }
      #endregion

      #region Members
      private int m_MonthId;
      #endregion

      #region Properties
      public int MonthId
      {
         get
         {
            return m_MonthId;
         }
         set
         {
            m_MonthId = value;
            this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("MonthId"));
         }
      }
      [XmlIgnore]
      public string CurrentMonthName
      {
         get
         {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.MonthId));
         }
      }
      #endregion

      #region CTOR
      public WorkingMonth(int month)
         : base()
      {
         this.MonthId = month;
      }

      private WorkingMonth()
         : base()
      {
      }
      #endregion

      #region IXmlSerializable Members
      public System.Xml.Schema.XmlSchema GetSchema()
      {
         return null;
      }

      public void ReadXml(System.Xml.XmlReader reader)
      {
         XmlSerializer keySerializer = new XmlSerializer(typeof(WorkingDay));

         this.MonthId = int.Parse(reader.GetAttribute("MonthId"));

         bool wasEmpty = reader.IsEmptyElement;
         reader.Read();

         if (wasEmpty)
            return;

         while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
         {
            this.Add((WorkingDay)keySerializer.Deserialize(reader));
            reader.MoveToContent();
         }
         reader.ReadEndElement();
      }

      public void WriteXml(System.Xml.XmlWriter writer)
      {
         writer.WriteAttributeString("MonthId", this.MonthId.ToString());

         XmlSerializer keySerializer = new XmlSerializer(typeof(WorkingDay));
         foreach (WorkingDay key in this)
         {
            keySerializer.Serialize(writer, key);
         }
      }
      #endregion

   }

   [Serializable]
   [XmlRoot("WorkingYear")]
   public class WorkingYear : ObservableCollection<WorkingMonth>
   {
      #region Properties
      [XmlAttribute]
      public int Year { get; set; }
      #endregion
   }

   [Serializable]
   public class WorkingLife : DictionaryViewModel<int, WorkingYear>
   {
   }
}
