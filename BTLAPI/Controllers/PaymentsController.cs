using BTLAPI.Models;
using BTLAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BTLAPI.Controllers
{
    public class PaymentsController : Controller
    {
        // GET: Payments
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var carts = (List<CartItem>)Session["cart"];
                Order newO = new Order();
                newO.UserID = long.Parse(Session["UserID"].ToString());
                newO.Date = DateTime.Now;
                newO.Status = 1;
                newO.Total = Decimal.Parse(Session["Total"].ToString());
                HttpResponseMessage response = GlobalVariables.webApiClient.PostAsJsonAsync("orders", newO).Result;
                Order order = response.Content.ReadAsAsync<Order>().Result;
                long OrderID = order.OrderID;
                List<OrderDetail> list = new List<OrderDetail>();

                foreach (var item in carts)
                {
                    OrderDetail oDetail = new OrderDetail();
                    oDetail.Quantity = item.Quantity;
                    oDetail.Price = (decimal)item.Price;
                    oDetail.ProductID = item.ProductID;
                    oDetail.OrderID = OrderID;
                    list.Add(oDetail);
                    HttpResponseMessage response1 = GlobalVariables.webApiClient.PostAsJsonAsync("orderdetails", oDetail).Result;

                }
                Session.Remove("cart");
                Session.Remove("Total");

            }
            return View();

        }
    }
}