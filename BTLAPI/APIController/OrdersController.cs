using BTLAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BTLAPI.APIController
{
    public class OrdersController : ApiController
    {
        DBContextcs db = new DBContextcs();
        public List<Order> GetOrders()
        {
            List<Order> orders = db.Orders.ToList();
            return orders;
        }
        [Route("api/orders/search")]
        [HttpGet]
        public List<Order> SearchOrder(int search)
        {
            List<Order> orders = db.Orders.ToList();
            if (search != null)
            {
                orders = orders.Where(row => row.OrderID != null &&
                    row.OrderID==search).ToList();
            }


            return orders;
        }
        public Order GetOrderById(int id)
        {
            Order order = db.Orders.Where(row => row.OrderID == id).FirstOrDefault();
            return order;
        }
        [HttpPost]
        public Order CreateOrders(Order newO)
        {
            db.Orders.Add(newO);
            db.SaveChanges();
            return newO;
        }

        //[HttpPut]
        //public void EditProduct(Product p)
        //{
        //    Product product = db.Products.Where(row => row.ProductID == p.ProductID).FirstOrDefault();
        //    product.ProductName = p.ProductName;
        //    product.Price = p.Price;
        //    product.BrandID = p.BrandID;
        //    product.Description = p.Description;
        //    product.Quantity = p.Quantity;
        //    product.Image = p.Image;
        //    db.SaveChanges();
        //}
        public void DeleteOrder(int id)
        {
            Order order= db.Orders.Where(row => row.OrderID == id).FirstOrDefault();
            db.Orders.Remove(order);
            db.SaveChanges();
        }


    }
}
