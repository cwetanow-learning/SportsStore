using Store.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Store.Domain.Models
{
    public class User : IUser
    {


        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Please enter email")]

        public string Email
        {
            get; set;
        }

        [Required(ErrorMessage = "Please enter name")]
        public string Name
        {
            get; set;
        }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter password")]
        [StringLength(int.MaxValue, MinimumLength = 6)]
        public string Password
        {
            get; set;
        }

        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [Required(ErrorMessage = "Please re-enter password")]
        public string ConfirmPassword
        {
            get; set;
        }

        [HiddenInput]
        public int UserId
        {
            get; set;
        }


        [Required(ErrorMessage = "Please enter username")]
        public string Username
        {
            get; set;
        }
    }
}
