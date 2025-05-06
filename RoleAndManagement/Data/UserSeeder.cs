using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoleAndManagement.Contants;

namespace RoleAndManagement.Data
{
    public class UserSeeder
    {
        public static async Task SeedUserAsync(IServiceProvider ServiceProvider)
        {
            var userManager = ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await CreateUserWithRole(userManager, "1097182147@qq.com", "Password12345!", Roles.Admin);
            await CreateUserWithRole(userManager, "JobSeeker@qq.com", "Password12345!", Roles.JobSeeker);
            await CreateUserWithRole(userManager, "Employer@qq.com", "Password12345!", Roles.Employer);

        }

        public static async Task CreateUserWithRole(
            UserManager<IdentityUser> userManager, 
            string email, 
            string password, 
            string role)
        {
            if(await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };

                var result = await userManager.CreateAsync(user, password);

                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"Failed creating user. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}