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

        public Previlege Previlege { get; set; }
        public static object Identity { get; internal set; }

        public User()
        {
            username = "";
            email = "";
            password = "";
            role = "user";
            Previlege = new Previlege();
        }
    }
}