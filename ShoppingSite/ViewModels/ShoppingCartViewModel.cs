using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingSite.Models;
namespace ShoppingSite.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
      //  public Item Item { get; set; }
        //public Cart Cart { get; set; }
        
    }
}