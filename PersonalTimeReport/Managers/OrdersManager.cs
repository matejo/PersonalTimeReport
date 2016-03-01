using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PersonalTimeReport
{
   public class OrdersManager : SingletonManager<OrdersManager>, IManager
   {
      #region Members
      private Orders m_Orders;
      private const string ORDERS_FILE = "Configuration\\Orders.xml";
      #endregion

      #region Properties
      public ManagerPriority Priority
      {
         get
         {
            return ManagerPriority.High;
         }
      }

      public string FullName
      {
         get
         {
            return this.GetType().FullName;
         }
      }

      public Orders Orders
      {
         get
         {
            return m_Orders;
         }
         set
         {
            m_Orders = value;
            this.OnPropertyChanged("Orders");
         }
      }
      #endregion

      #region Methods
      internal void Save()
      {
         try
         {
            Utilities.WriteToXmlFile<Orders>(ORDERS_FILE, Orders);
         }
         catch { }
      }

      public override void Load()
      {
         try
         {
            Orders = Utilities.ReadFromXmlFile<Orders>(ORDERS_FILE);
         }
         catch { }
         if (Orders == null)
         {
            Orders = new Orders();
         }
      }

      public override void Start()
      {
      }

      public override void Stop()
      {
      }

      internal void AddOrUpdate(string id, string name)
      {
         Order order = this.Orders.FirstOrDefault(or => or.Id == id);
         if (order == null)
         {
            this.Orders.Add(new Order() { Id = id, Name = name });
         }
         else
         {
            order.Name = name;
         }
         this.Save();
      }
      #endregion

      internal void Delete(string id)
      {
         Order order = this.Orders.FirstOrDefault(or => or.Id == id);
         if (order != null)
         {
            this.Orders.Remove(order);
         }
         this.Save();
      }
   }
}