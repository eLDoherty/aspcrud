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

        [Required(ErrorMessage = "Please select an image!")]
       // [RegularExpression(@"([^\s]+(\.(?i)(jpg|png|gif|bmp))$)", ErrorMessage = "Only Image files allowed (JPG & PNG).")]
        public string thumbnail { get; set; }
    }

}