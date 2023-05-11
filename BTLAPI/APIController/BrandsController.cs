using BTLAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BTLAPI.APIController
{
    public class BrandsController : ApiController
    {
        DBContextcs db = new DBContextcs();
        public List<Brands> GetBrands()
        {
            List<Brands> brands = db.Brands.ToList();
            return brands;
        }
        public Brands GetBrandByID(long id)
        {
            Brands brand = db.Brands.Where(row => row.BrandID == id).FirstOrDefault();
            return brand;
        }
    }
}
