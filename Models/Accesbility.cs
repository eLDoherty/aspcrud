using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace learnnet.Models
{
    public class Accesbility
    {
        [Key]
        public int id { get; set; }

        public int productPage { get; set; }

        public int categoryPage { get; set; }

        public int mediaPage { get; set; }

        public int draftPage { get; set; }

        [ForeignKey("user_id")]
        public int user_id { get; set; }

        public virtual User User { get; set; }

        public Accesbility()
        {
            productPage = 0;
            categoryPage = 0;
            mediaPage = 0;
            draftPage = 0;
        }
    }
}