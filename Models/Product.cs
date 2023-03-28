using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace learnnet.Models

{
    public class Product
    {
        public int id { get; set; }

        public string name { get; set; }

        public decimal price { get; set; }

        public string slug { get; set; }

        public string thumbnail { get; set; }
    }

}