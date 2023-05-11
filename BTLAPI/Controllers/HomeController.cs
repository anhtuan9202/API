using BTLAPI.APIController;
using BTLAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BTLAPI.Controllers
{

    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Product> products;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("products").Result;
            products = response.Content.ReadAsAsync<List<Product>>().Result;
            return View(products);
            
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("users?email=" + user.Email + "").Result;
                var check = response.Content.ReadAsAsync<User>().Result;
                if (check == null)
                {
                    HttpResponseMessage response1 = GlobalVariables.webApiClient.PostAsJsonAsync("users", user).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(user);
                    }
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("users?email=" + email).Result;
                var data = response.Content.ReadAsAsync<User>().Result;
                if (data != null && data.Password == UsersController.GetMD5(password))
                {
                    //add session
                    Session["FullName"] = data.FullName;
                    Session["Email"] = data.Email;
                    Session["UserID"] = data.UserID;
                    if (data.isAdmin == true)
                    {
                        Session["IsAdmin"] = data.isAdmin;
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }


    }
}