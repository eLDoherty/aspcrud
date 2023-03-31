using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace learnnet.Models

{
    public class Category
    {
        public int cat_id { get; set; }
        public string category { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}