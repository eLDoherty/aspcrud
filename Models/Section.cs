﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace learnnet.Models
{
    public class Section
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }


        public Section()
        {
            name = "";
        }
    }
}