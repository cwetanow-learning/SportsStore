using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.WebUI.Models
{
    public class LoginViewModel
    {
        [Required]
        public virtual string Username { get; set; }

        [Required]
        public virtual string Password { get; set; }
    }
}