using System.ComponentModel.DataAnnotations;

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
    }

}