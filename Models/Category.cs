using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace learnnet.Models

{
    public class Category
    {
        public int id { get; set; }
        public string category { get; set; }

        [ForeignKey("product")]
        public int product_id { get; set; }

        public virtual Product Product { get; set; }
    }
}