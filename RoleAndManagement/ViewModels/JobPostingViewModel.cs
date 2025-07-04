using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleAndManagement.ViewModels
{
    public class JobPostingViewModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Classification { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Company { get; set; } = string.Empty;
        [Required]
        public string Location { get; set; } = string.Empty;

    }
}