using Microsoft.AspNetCore.Identity;
using RadioCabs.Models;
namespace RadioCabs.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            string[] roleNames = { "Company", "Driver", "Admin" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            await EnsureAdminUserExists(userManager, configuration);
        }

        private static async Task EnsureAdminUserExists(UserManager<User> userManager, IConfiguration configuration)
        {
            var adminEmail = configuration["AdminUser:Email"];
            var adminPassword = configuration["AdminUser:Password"];

            if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
            {
                throw new Exception("Admin email or password is not configured properly.");
            }

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new User { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true, UserType = "Admin" };
                var result = await userManager.CreateAsync(newAdmin, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }
    }
}
