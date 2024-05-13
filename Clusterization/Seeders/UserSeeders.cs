﻿using Domain.Entities.Customers;
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
                if (await _userManager.FindByEmailAsync("sladkovsky.george@gmail.com") != null) return;

                var newuser = new Customer { UserName = "Admin", Email = "sladkovsky.george@gmail.com" };
                var result = await _userManager.CreateAsync(newuser, "94519451Ge11$");

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newuser);
                await _userManager.ConfirmEmailAsync(newuser, token);

                using (RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>())
                {
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Visitor))
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Visitor));

                    if (await _roleManager.RoleExistsAsync(UserRoles.Visitor))
                    {
                        await _userManager.AddToRoleAsync(newuser, UserRoles.Visitor);
                    }

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
