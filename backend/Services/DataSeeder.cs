using HospitalSystemAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystemAPI.Services
{
    public class DataSeeder
    {
        private static readonly List<string> Roles = new List<string> { "genAdmin","emergAdmin", "Doctor", "Patient" };

        public static async Task SeedAdminsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Roles
            await SeedRolesAsync(roleManager);

            var generalAdmin = new ApplicationUser { UserName = "admin_general", Email = "admin_general@gmail.com", EmailConfirmed = true };
            var emergencyAdmin = new ApplicationUser { UserName = "admin_emergency", Email = "admin_emergency@gmail.com", EmailConfirmed = true };
            
            var existingGenAdmin = await userManager.FindByEmailAsync(generalAdmin.Email);
            if (existingGenAdmin == null)
            {
                await userManager.CreateAsync(generalAdmin, "AdGen#1");
                await userManager.AddToRoleAsync(generalAdmin, "genAdmin");
            }

            var existingEmergAdmin = await userManager.FindByEmailAsync(emergencyAdmin.Email);
            if (existingEmergAdmin == null)
            {
                await userManager.CreateAsync(emergencyAdmin, "AdEmerg#1");
                await userManager.AddToRoleAsync(emergencyAdmin, "genAdmin");
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
