using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace learnnet.Models
{
    public class Accessbility
    {
      
        public int sectionId { get; set; }

        public int addition { get; set; }

        public int edition { get; set; }

        public int deletion { get; set; }

        public string role  { get; set; }

        [ForeignKey("user_id")]
        public int userId { get; set; }

        public virtual User User { get; set; }

        public Accessbility()
        {
            sectionId = 0;
            addition = 0;
            edition = 0;
            deletion = 0;
            userId = 0;
            role = "";
        }
    }
}