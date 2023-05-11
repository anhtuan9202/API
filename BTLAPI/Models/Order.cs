using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BTLAPI.Models
{
    public class Order
    {
        [Key]
        public long OrderID { get; set; }
        public long UserID { get; set; }
        public DateTime Date { get; set; }
        public Decimal Total { get; set; }
        public byte Status { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}