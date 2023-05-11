    using BTLAPI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace BTLAPI.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index(string search = "", string sortColumn = "ProductID", string IconClass = "fa-sort-asc")
        {
            //search
            List<Product> products;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("products/search?search=" + search + "").Result;
            products = response.Content.ReadAsAsync<List<Product>>().Result;
            ViewBag.Search = search;

            //sort
            ViewBag.SortColumn = sortColumn;
            ViewBag.IconClass = IconClass;
            //if(sortColumn == "ProductID")
            //{
            //    if(IconClass == "fa-sort-asc")
            //    {
            //        products = products.OrderBy(row => row.ProductID).ToList();
            //    }
            //    else
            //    {
            //        products = products.OrderByDescending(row => row.ProductID).ToList();
            //    }
            //}
            //else if (sortColumn == "ProductName")
            //{
            //    if (IconClass == "fa-sort-asc")
            //    {
            //        products = products.OrderBy(row => row.ProductName).ToList();
            //    }
            //    else
            //    {
            //        products = products.OrderByDescending(row => row.ProductName).ToList();
            //    }
            //}
            //else if (sortColumn == "Price")
            //{
            //    if (IconClass == "fa-sort-asc")
            //    {
            //        products = products.OrderBy(row => row.Price).ToList();
            //    }
            //    else
            //    {
            //        products = products.OrderByDescending(row => row.Price).ToList();
            //    }
            //}
            return View(products);
        }
        public ActionResult Detail(int id)
        {
            Product product;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("products?id=" + id + "").Result;
            product = response.Content.ReadAsAsync<Product>().Result;
            return View(product);
        }

        public ActionResult Create()
        {
            //brands
            List<Brands> brands;
            HttpResponseMessage response1 = GlobalVariables.webApiClient.GetAsync("brands").Result;
            ViewBag.Brands = response1.Content.ReadAsAsync<List<Brands>>().Result;

            return View();
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string path = Server.MapPath("~/Images/" + filename);
                    p.Image = filename;
                    file.SaveAs(path);
                }
            }
            else
            {
                // no file uploaded, set default image path
                p.Image = @"default.png";
            }

            HttpResponseMessage response = GlobalVariables.webApiClient.PostAsJsonAsync("products", p).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(p);
            }
        }




        public ActionResult Edit(int id)
        {
            //brands
            List<Brands> brands;
            HttpResponseMessage response2 = GlobalVariables.webApiClient.GetAsync("brands").Result;
            ViewBag.Brands = response2.Content.ReadAsAsync<List<Brands>>().Result;
            //
            Product product;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("products?id=" + id + "").Result;
            product = response.Content.ReadAsAsync<Product>().Result;
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product updatedP)
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files["ImageUpLoad"];
                if (file != null && file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string path = Server.MapPath("~/Images/" + filename);
                    updatedP.Image = filename;
                    file.SaveAs(path);
                }
            }
            else
            {
                // no file uploaded, set default image path
                updatedP.Image = updatedP.Image;
            }
            HttpResponseMessage response = GlobalVariables.webApiClient.PutAsJsonAsync("products", updatedP).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(updatedP);
            }
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.DeleteAsync("products?id=" + id + "").Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}