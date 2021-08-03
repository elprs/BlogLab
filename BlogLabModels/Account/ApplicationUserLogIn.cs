using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogLabModels.Account
{
    public class ApplicationUserLogIn
    {

        [Required(ErrorMessage = "UserName is required")]
        [MinLength(5, ErrorMessage = "It must be at least 5-20 chars")]
        [MaxLength(20, ErrorMessage = "It must be at least 5-20 chars")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        [MinLength(10, ErrorMessage = "It must be at least 10-50 chars")]
        [MaxLength(50, ErrorMessage = "It must be at least 10-50 chars")]
        public string Password { get; set; }
    
    }
}
