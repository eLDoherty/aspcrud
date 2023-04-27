using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace learnnet.Models
{
    public class User
    {
        public int id { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        public string role { get; set; }

        public Accessbility Accesbility { get; set; }

        public User()
        {
            username = "";
            email = "";
            password = "";
            role = "user";
            Accesbility = new Accessbility();
        }
    }
}