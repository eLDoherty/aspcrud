using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace learnnet.Models
{
    public class ProductCategory
    {
        public int product_id { get; set;}
        public Product Product;
        
        public int category_id { get; set; }
        public Category Category;
    }
}