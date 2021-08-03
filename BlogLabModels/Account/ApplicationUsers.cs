using System;
using System.Collections.Generic;
using System.Text;

namespace BlogLabModels.Account
{
    public class ApplicationUsers
    {
        public int ApplicationUserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
