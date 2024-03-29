using Domain.Entities.Customers;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Identity;

namespace Clusterization.Seeders
{
    public static class UserSeeders
    {
        public static async Task Configure(IServiceProvider serviceProvider)
        {
            using (UserManager<Customer> _userManager = serviceProvider.GetRequiredService<UserManager<Customer>>())
            {
                if (await _userManager.FindByEmailAsync("admin@gmail.com") != null) return;

                var newuser = new Customer { UserName = "Admin", Email = "admin@gmail.com" };
                var result = await _userManager.CreateAsync(newuser, "41924192Ge11$");

                using (RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>())
                {
                    if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                    if (await _roleManager.RoleExistsAsync(UserRoles.User))
                    {
                        await _userManager.AddToRoleAsync(newuser, UserRoles.User);
                    }

                    if (!await _roleManager.RoleExistsAsync(UserRoles.Moderator))
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Moderator));

                    if (await _roleManager.RoleExistsAsync(UserRoles.Moderator))
                    {
                        await _userManager.AddToRoleAsync(newuser, UserRoles.Moderator);
                    }
                }
            }
        }
    }
}
