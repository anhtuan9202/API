using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BTLAPI.Models
{
    public class Product
    {
        [Key]
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Boolean isDeleted { get; set; }
        public Nullable<long> BrandID { get; set; }
        public virtual Brands Brand { get; set; }

    }
}