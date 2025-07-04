using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;



namespace RoleAndManagement.Models
{
    public enum JobType
    {
        Accounting,
        BankingFinancial,
        CustomerService,
        Management,
        Design,
        HealthCare,
        Science,
        InformationTechnology,
        Sale,
        Sport
    }
    public class JobPosting
    {
        [Key]
        public int Id { get; set; }
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

        public DateTime PostedDate { get; set; } = DateTime.UtcNow;

        public bool IsApproved { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}