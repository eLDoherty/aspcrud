using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace learnnet.Models

{
    public class Image
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }

        public string uri { get; set; }

        public Image()
        {
            name = "";        }
    }
}