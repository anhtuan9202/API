using BTLAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BTLAPI.Controllers
{
    public class OrderController : Controller
    {
        
        public ActionResult Index()
        {
            //search
            List<Order> orders;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("orders").Result;
            orders = response.Content.ReadAsAsync<List<Order>>().Result;

            return View(orders);
        }
    }
}