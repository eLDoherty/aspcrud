using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using learnnet.Models;

namespace learnnet.Models

{
    public class Product
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required, RegularExpression("^[0-9.]+$")]
        public decimal price { get; set; }

        public string slug { get; set; }

        public string thumbnail { get; set; }

        public string description { get; set; }

        public string status { get; set; }

        public string trending { get; set; }

        public ICollection<Category> Categories { get; set; }
    }

}