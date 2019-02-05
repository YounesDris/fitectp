using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class UserAccount
    {
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50, ErrorMessage = "User name cannot be longer than 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
    }
}