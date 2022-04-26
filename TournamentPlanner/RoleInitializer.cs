using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TournamentPlanner.DAL.Entities;

namespace TournamentPlanner
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("player") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("player"));
            }   
            if (await roleManager.FindByNameAsync("guest") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("guest"));
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail,  };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                    admin.EmailConfirmed = true;
                    await userManager.UpdateAsync(admin);
                }
            }
            
            User adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (!await userManager.IsInRoleAsync(adminUser, "admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "admin");
                adminUser.EmailConfirmed = true;
                await userManager.UpdateAsync(adminUser);
            }
        }
    }
}
