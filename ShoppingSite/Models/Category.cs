﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingSite.Models
{
    public class Category
    {
        public int categoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
    }
}