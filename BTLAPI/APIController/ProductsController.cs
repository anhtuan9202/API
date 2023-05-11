using BTLAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace BTLAPI.APIController
{
    public class ProductsController : ApiController
    {
        DBContextcs db = new DBContextcs();
        public List<Product> GetProducts(bool incluDeleted = false)
        {
            if (incluDeleted)
            {
                List<Product> products = db.Products.ToList();
                return products;
            }
            else
            {
                List<Product> products = db.Products.Where(row => row.isDeleted == false).ToList();
                return products;
            }
            
        }

        [Route("api/products/search")]
        [HttpGet]
        public List<Product> SearchProduct(string search = null)
        {
            List<Product> products = db.Products.Where(p => p.isDeleted != true).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.ProductName != null &&
                    p.ProductName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            return product;
        }
        [HttpPost]
        public void CreateProduct(Product newP)
        {
            db.Products.Add(newP);
            db.SaveChanges();
        }

        [HttpPut]
        public void EditProduct(Product p)
        {
            Product product = db.Products.Where(row => row.ProductID == p.ProductID).FirstOrDefault();
            product.ProductName = p.ProductName;
            product.Price = p.Price;
            product.BrandID = p.BrandID;
            product.Description = p.Description;
            //product.Quantity = p.Quantity;
            product.Image = p.Image;
            db.SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            Product product = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            product.isDeleted = true;
            db.SaveChanges();
        }


        //[Route("api/products/sort/sortnamedes")]
        //[HttpGet]
        //public List<Product> SortByProductNameDes(List<Product> products)
        //{
        //    return products.OrderByDescending(p => p.ProductName).ToList();
        //}




        //[Route("api/products/sort/sortname/{ascending}")]
        //[HttpGet]
        //public List<Product> SortByProductName(List<Product> products, bool ascending)
        //{
        //    if (ascending)
        //    {
        //        return products.OrderBy(p => p.ProductName).ToList();
        //    }
        //    else
        //    {
        //        return products.OrderByDescending(p => p.ProductName).ToList();
        //    }
        //}
        //[Route("api/products/sort/sortid/{ascending}")]
        //[HttpGet]
        //public List<Product> SortByProductID(List<Product> products, bool ascending)
        //{
        //    if (ascending)
        //    {
        //        return products.OrderBy(p => p.ProductID).ToList();
        //    }
        //    else
        //    {
        //        return products.OrderByDescending(p => p.ProductID).ToList();
        //    }
        //}
        //[Route("api/products/sort/sortprice/{ascending}")]
        //[HttpGet]
        //public List<Product> SortByPrice(List<Product> products, bool ascending)
        //{
        //    if (ascending)
        //    {
        //        return products.OrderBy(p => p.Price).ToList();
        //    }
        //    else
        //    {
        //        return products.OrderByDescending(p => p.Price).ToList();
        //    }
        //}
    }
}
