using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace learnnet.Models

{
    public class Category
    {
        [Key]
        public int id { get; set; }

        [Required, AllowHtml]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special characters are not allowed.")]
        public string category { get; set; }

        public List<ProductCategory>productCategories { get; set; }

        public Category()
        {
            category = "";
            productCategories = new List<ProductCategory>();
        }
    }
}