using BTLAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTLAPI.Models;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Mail;
using System.Net;

namespace BTLAPI.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            return View(cart);
        }
        public RedirectToRouteResult AddToCart(int ProductID)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<CartItem>();
            }
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            if (cart.FirstOrDefault(m => m.ProductID == ProductID) == null)
            {
                Product product;
                HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("products?id=" + ProductID + "").Result;
                product = response.Content.ReadAsAsync<Product>().Result;
                CartItem newItem = new CartItem();
                newItem.ProductID = ProductID;
                newItem.ProductName = product.ProductName;
                newItem.Quantity = 1;
                newItem.Price = Convert.ToDouble(product.Price);
                cart.Add(newItem);
            }
            else
            {
                CartItem cartItem = cart.FirstOrDefault(m => m.ProductID == ProductID);
            }
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult Update(int ProductID, int txtQuantity)
        {
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            CartItem item = cart.FirstOrDefault(m => m.ProductID == ProductID);
            if (item != null)
            {
                item.Quantity = txtQuantity;
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult DelCartItem(int ProductID)
        {
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            CartItem item = cart.FirstOrDefault(m => m.ProductID == ProductID);
            if (item != null)
            {
                cart.Remove(item);
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");

        }

        public ActionResult Order(string Email, string Phone)
        {
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            string sMsg = "<html><body><table border='1'><caption>Thông tin đặt hàng</caption><tr><th>STT</th><th>Tên hàng</th><th>Số lượng</th><th>Đơn giá</th><th>Thành tiền</th></tr>";
            int i = 0;
            double total = 0;
            foreach (CartItem item in cart)
            {
                i++;
                sMsg += "<tr>";
                sMsg += "<td>" + i.ToString() + "</td>";
                sMsg += "<td>" + item.ProductName + "</td>";
                sMsg += "<td>" + item.Quantity.ToString() + "</td>";
                sMsg += "<td>" + item.Price.ToString() + "</td>";
                sMsg += "<td>" + String.Format("{0:#,###}", item.Quantity * item.Price) + "</td>";
                sMsg += "<tr>";
                total += item.Quantity * item.Price;
            }
            sMsg += "<tr><th colspan='5'> Tổng cộng: "
                + String.Format("{0:#,### VND}", total) + "</th></tr></table>";
            Session["Total"] = total;
            MailMessage mail = new MailMessage("tatuan0902@gmail.com", Session["Email"].ToString(), "Thông tin đơn hàng", sMsg);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("tatuan0902@gmail.com", "oyvwbnytmdfqrvmx");
            mail.IsBodyHtml = true;
            client.Send(mail);
            return RedirectToAction("Index", "Payments");
        }

    }
}