using BTLAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BTLAPI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            List<User> users;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("users").Result;
            users = response.Content.ReadAsAsync<List<User>>().Result;
            return View(users);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User u)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.PostAsJsonAsync("users", u).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(u);
            }
        }
        public ActionResult Edit(int id)
        {
            User user;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("users/getid?id=" + id + "").Result;
            user = response.Content.ReadAsAsync<User>().Result;
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User updateU)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.PutAsJsonAsync("users", updateU).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(updateU);
            }
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.DeleteAsync("users?id=" + id + "").Result;
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