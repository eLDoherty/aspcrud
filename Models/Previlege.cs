using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace learnnet.Models
{
    public class Previlege
    {
        [Key]
        public int id { get; set; }

        public int canCreate { get; set; }

        public int canEdit { get; set; }

        public int canDelete { get; set; }

        [ForeignKey("user_id")]
        public int user_id { get; set; }

        public virtual User User { get; set; }

        public Previlege()
        {
            canCreate = 0;
            canEdit = 0;
            canDelete = 0;
        }
    }
}