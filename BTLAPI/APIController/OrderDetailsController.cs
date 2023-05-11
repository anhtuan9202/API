using BTLAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BTLAPI.APIController
{
    public class OrderDetailsController : ApiController
    {
        DBContextcs db = new DBContextcs();
        //public void CreateOrderDetails(List<OrderDetail> list)
        //{
        //    db.OrderDetails.AddRange(list);
        //    db.SaveChanges();
        //}
        public List<OrderDetail> GetOrderDetails()
        {
            List<OrderDetail> orderDetails = db.OrderDetails.ToList();
            return orderDetails;
        }
        public void CreateOrderDetails(OrderDetail orderDetails)
        {
            db.OrderDetails.Add(orderDetails);
            db.SaveChanges();
        }
    }
}
