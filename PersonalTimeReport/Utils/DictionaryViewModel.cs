using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PersonalTimeReport
{
   /// <summary>
   /// Define a class that contains structure and behavior for dictionaryViewModel.
   /// </summary>
   public class DictionaryViewModel<TKey, TValue> : Dictionary<TKey, TValue>, INotifyPropertyChanged, IXmlSerializable
   {
      /// <summary>
      /// Occur when a property of the dictionary change.
      /// </summary>
      public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChanged(string propertyName)
      {
         PropertyChangedEventHandler handler = this.PropertyChanged;
         if (handler != null)
         {
            handler(this, new PropertyChangedEventArgs(propertyName));
         }
      }

      public new void Add(TKey key, TValue value)
      {
         base.Add(key, value);
         OnPropertyChanged(string.Empty);

      }
      
      public virtual new TValue this[TKey key]
      {
         get
         {
            TValue val;
            return this.TryGetValue(key, out val) ? val : default(TValue);
         }
         set
         {
            base[key] = value;
            OnPropertyChanged(string.Empty);
         }
      }

      #region IXmlSerializable Members
      public System.Xml.Schema.XmlSchema GetSchema()
      {
         return null;
      }

      public void ReadXml(System.Xml.XmlReader reader)
      {
         XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
         XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

         bool wasEmpty = reader.IsEmptyElement;
         reader.Read();

         if (wasEmpty)
            return;

         while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
         {
            reader.ReadStartElement("item");

            //reader.ReadStartElement("key");
            TKey key = (TKey)keySerializer.Deserialize(reader);
            //reader.ReadEndElement();

            //reader.ReadStartElement("value");
            TValue value = (TValue)valueSerializer.Deserialize(reader);
            //reader.ReadEndElement();

            this.Add(key, value);

            reader.ReadEndElement();
            reader.MoveToContent();
         }
         reader.ReadEndElement();
      }

      public void WriteXml(System.Xml.XmlWriter writer)
      {
         XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
         XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

         foreach (TKey key in this.Keys)
         {
            writer.WriteStartElement("item");

            //writer.WriteStartElement("key");
            keySerializer.Serialize(writer, key);
            //writer.WriteEndElement();

            //writer.WriteStartElement("value");
            TValue value = this[key];
            valueSerializer.Serialize(writer, value);
            //writer.WriteEndElement();

            writer.WriteEndElement();
         }
      }
      #endregion
   }
}
