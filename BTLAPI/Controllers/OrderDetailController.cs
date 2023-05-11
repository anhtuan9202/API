using BTLAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BTLAPI.Controllers
{
    public class OrderDetailController : Controller
    {
        public ActionResult Index(int? OrderID)
        {
            // Order
            List<Order> orders;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("orders").Result;
            orders = response.Content.ReadAsAsync<List<Order>>().Result;
            // Product
            List<Product> products;
            HttpResponseMessage response1 = GlobalVariables.webApiClient.GetAsync("products?includeleted=true").Result;
            products = response1.Content.ReadAsAsync<List<Product>>().Result;
            // User
            List<User> users;
            HttpResponseMessage response2 = GlobalVariables.webApiClient.GetAsync("users").Result;
            users = response2.Content.ReadAsAsync<List<User>>().Result;
            // OrderDetails
            List<OrderDetail> orderDetails;
            HttpResponseMessage response3 = GlobalVariables.webApiClient.GetAsync("orderdetails").Result;
            orderDetails = response3.Content.ReadAsAsync<List<OrderDetail>>().Result;

            var main = from h in orders
                       join k in users on h.UserID equals k.UserID
                       where(h.OrderID == OrderID)
                       select new ViewModel
                       {
                           order = h,
                           user = k
                       };
            var sub = from c in orderDetails
                      join s in products on c.ProductID equals s.ProductID
                      where (c.OrderID == OrderID)
                      select new ViewModel
                      {
                          orderDetail = c,
                          product = s,
                          Total = Convert.ToDouble(c.Quantity * c.Price)
                      };
            ViewBag.Main = main;
            ViewBag.Sub = sub;

            return View();
        }
    }
}
