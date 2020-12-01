using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthMvc.Models
{ 
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string ShipperCity { get; set; }
        public Boolean IsShipped { get; set; }

        #region Helpers  
        
        private static List<Order> orderList = new List<Order>
        {
            new Order {OrderID = 10248, CustomerName = "John Smith", ShipperCity = "Allen", IsShipped = true },
            new Order {OrderID = 10249, CustomerName = "Ann Corney", ShipperCity = "Plano", IsShipped = false},
            new Order {OrderID = 10250, CustomerName = "Tamara Migus", ShipperCity = "Dallas", IsShipped = false },
            new Order {OrderID = 10251, CustomerName = "Lina Mareny", ShipperCity = "Frisco", IsShipped = false},
            new Order {OrderID = 10252, CustomerName = "Ron Buren", ShipperCity = "Dubai", IsShipped = true}
        };

        public static List<Order> CreateOrders()
        { 
            return orderList;
        }

        public static Order GetOrderByID(int id)
        { 
            Order myOrder = orderList.Single(x => x.OrderID == id);

            return myOrder;
        }

        public static List<Order> AddOrder(Order order)
        {
            Order item = orderList[orderList.Count-1];
            order.OrderID = item.OrderID + 1;

            orderList.Add(order); 

            return orderList;
        }

        public static List<Order> UpdateOrder(Order order) 
        {  
            foreach (var item in orderList.Where(w => w.OrderID == order.OrderID))
            {
                item.CustomerName = order.CustomerName;
                item.ShipperCity = order.ShipperCity;
                item.IsShipped = order.IsShipped;
            } 

            return orderList;
        }

        public static List<Order> DeleteOrder(int id)
        { 
            var item = orderList.First(x => x.OrderID == id);
            orderList.Remove(item); 

            return orderList;
        }

        #endregion
    }
}