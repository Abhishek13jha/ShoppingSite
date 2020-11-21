using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingSite.ViewModels
{
    public class ShoppingCartRemoveViewModel
    {
        public decimal CartTotal { get; set; }
        public int CartCount { get; set; }
        public String Message { get; set; }
        public int ItemCount { get; set; }
        public int DeleteId { get; set; }
    }
}