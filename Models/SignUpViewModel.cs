using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class SignUpViewModel
    {
        // Customer fields
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int Phone { get; set; }

        // User fields
        public string Password { get; set; }
        public string Type { get; set; } = "Customer"; // default
    }
}