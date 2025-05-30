using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleAndManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}