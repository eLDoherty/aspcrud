using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace learnnet.Models

{
    public class Product
    {

        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [RegularExpression("^[0-9]+$")]
        public int Price { get; set; }
    }
}