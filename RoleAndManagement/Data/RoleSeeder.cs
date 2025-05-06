using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RoleAndManagement.Contants;

namespace RoleAndManagement.Data
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider ServiceProvider)
        {
            var roleManager = ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if(!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            if(!await roleManager.RoleExistsAsync(Roles.JobSeeker))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.JobSeeker));
            }

            if(!await roleManager.RoleExistsAsync(Roles.Employer))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Employer));
            }
        }
    }
}