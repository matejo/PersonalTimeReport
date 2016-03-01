using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PersonalTimeReport
{
   [Serializable]
   public class Order : ViewModel
   {
      #region Members
      private string m_Id;
      private string m_Name;
      #endregion

      #region Properties
      [XmlAttribute]
      public string Id
      {
         get
         {
            return m_Id;
         }
         set
         {
            m_Id = value;
            this.OnPropertyChanged("Id");
         }
      }
      [XmlAttribute]
      public string Name
      {
         get
         {
            return m_Name;
         }
         set
         {
            m_Name = value;
            this.OnPropertyChanged("Name");
         }
      }
      #endregion
   }

   [Serializable]
   [XmlRoot("Orders")]
   public class Orders : ObservableCollection<Order>
   {
      #region Operation
      public Order this[string id]
      {
         get
         {
            return this.FirstOrDefault(w => w.Id == id);
         }
      }
      #endregion
   }

   [Serializable]
   public class WorkItem : ViewModel, IXmlSerializable
   {
      private TimeSpan m_RealHours;
      private TimeSpan m_DeliveredHours;

      public TimeSpan RealHours
      {
         get
         {
            return m_RealHours;
         }
         set
         {
            m_RealHours = value;
            this.OnPropertyChanged("RealHours");
         }
      }
      public TimeSpan DeliveredHours
      {
         get
         {
            return m_DeliveredHours;
         }
         set
         {
            m_DeliveredHours = value;
            this.OnPropertyChanged("DeliveredHours");
         }
      }

      #region IXmlSerializable
      public System.Xml.Schema.XmlSchema GetSchema()
      {
         throw null;
      }

      public void ReadXml(System.Xml.XmlReader reader)
      {
         bool wasEmpty = reader.IsEmptyElement;
         
         this.RealHours = TimeSpan.Parse(reader.GetAttribute("RealHours"));
         this.DeliveredHours = TimeSpan.Parse(reader.GetAttribute("DeliveredHours"));

         reader.Read();

         if (wasEmpty)
            return;
      }

      public void WriteXml(System.Xml.XmlWriter writer)
      {
         writer.WriteAttributeString("RealHours", this.RealHours.ToString());
         writer.WriteAttributeString("DeliveredHours", this.DeliveredHours.ToString());
      }
      #endregion
   }
}
