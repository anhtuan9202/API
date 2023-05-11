using BTLAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using BTLAPI.Models;
using System.Net.Http;

namespace BTLAPI.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        public ActionResult Index()
        {
            List<Brands> brands;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("brands").Result;
            brands = response.Content.ReadAsAsync<List<Brands>>().Result;
            return View(brands);
        }
    }
}