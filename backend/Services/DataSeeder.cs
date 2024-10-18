using HospitalSystemAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystemAPI.Services
{
    public class DataSeeder
    {
        private static readonly List<string> Roles = new List<string> { "genAdmin", "Doctor", "Patient" };

        public static async Task SeedAdminsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Roles
            await SeedRolesAsync(roleManager);

            var generalAdmin = new ApplicationUser { UserName = "admin_general", Email = "admin_general@gmail.com", EmailConfirmed = true };
            
            var existingGenAdmin = await userManager.FindByEmailAsync(generalAdmin.Email);
            if (existingGenAdmin == null)
            {
                await userManager.CreateAsync(generalAdmin, "AdGen#1");
                await userManager.AddToRoleAsync(generalAdmin, "genAdmin");
            }

        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
