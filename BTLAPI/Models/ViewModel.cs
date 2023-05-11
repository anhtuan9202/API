using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BTLAPI.Models
{
    public class ViewModel
    {
        public OrderDetail orderDetail { get; set; }
        public Order order { get; set; }
        public Brands brand { get; set; }
        public Product product { get; set; }
        public User user { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.##0}")]
        public Double Total { get; set; }
    }
}