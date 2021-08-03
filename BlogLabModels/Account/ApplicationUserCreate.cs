using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogLabModels.Account
{
    public class ApplicationUserCreate : ApplicationUserLogIn
    {


        [MinLength(10, ErrorMessage = "It must be at least 10-30 chars")]
        [MaxLength(30, ErrorMessage = "It must be at least 10-30 chars")]

        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email fromat")]
        [MaxLength(30, ErrorMessage = "It must be at most 30 chars")]
        public string Email { get; set; }
    

    }
}
