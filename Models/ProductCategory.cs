using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace learnnet.Models
{
    public class ProductCategory
    {
        public int product_id { get; set; }
        public Product Product { get; set; }

        public int cat_id { get; set; }
        public Category Category { get; set; }
    }
}