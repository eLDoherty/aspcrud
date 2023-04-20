using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using learnnet.Models;

namespace learnnet.Models

{
    public class Product
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required, RegularExpression("^[0-9.]+$")]
        public decimal price { get; set; }

        public string slug { get; set; }

        [Required]
        public string thumbnail { get; set; }


        [Required, AllowHtml]
        public string description { get; set; }

        public string status { get; set; }

        public string trending { get; set; }

        public List<ProductCategory> productCategories { get; set; }

        public string country { get; set; }

        public Product() {
            name="";
            price = 0;
            slug = "";
            thumbnail = "";
            description = "";
            status = "draft";
            trending = "0";
            productCategories = new List<ProductCategory>();
        }
    }
}