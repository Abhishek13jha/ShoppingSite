using ShoppingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingSite.ViewModels
{
    public class RandomViewModel
    {
        public Category Category { get; set; }
        public Item Item { get; set; }
        public Producer Producer { get; set; }
    }
}