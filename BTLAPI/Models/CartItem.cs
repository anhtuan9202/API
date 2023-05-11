using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTLAPI.ViewModels
{
    [Serializable]
    public class CartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total
        {
            get { return Price * Quantity; }
        }
    }
}