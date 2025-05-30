using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleAndManagement.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool RememberMe { get; set; }
    }
}