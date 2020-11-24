using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingSite.Models
{
  //  [Bind(Exclude = "ItemId")]
    public class Item
    {
        [ScaffoldColumn(false)]
        
        public int ItemId { get; set; }
        [Required(ErrorMessage = "*Required Field")]
        public string ItemName { get; set; }
        [Required(ErrorMessage = "*Required Field")]
        [Range(50, 10000, ErrorMessage = "Must be between 50 to 10000")]
        public float Price { get; set; }
        [DisplayName("Item Photo Url")]
        public string ItemUrl { get; set; }
        public Category Category { get; set; }
        public Producer Producer { get; set; }
        [DisplayName("Producer")]
        public int ProducerId { get; set; }
        [DisplayName("Category")]
        public int categoryId { get; set; }
    }
}