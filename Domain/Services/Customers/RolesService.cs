using Domain.Entities.Customers;
using Domain.Exceptions;
using Domain.Interfaces.Customers;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Customers
{
    public class RolesService:IRolesService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStringLocalizer<ErrorMessages> _localizer;

        public RolesService(UserManager<Customer> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IStringLocalizer<ErrorMessages> localizer)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _localizer = localizer;
        }

        public async Task AddModerator(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotFound], HttpStatusCode.BadRequest);

            if (!await _roleManager.RoleExistsAsync(UserRoles.Moderator))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Moderator));

            if (await _roleManager.RoleExistsAsync(UserRoles.Moderator))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Moderator);
            }
        }

        public async Task RemoveModerator(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotFound], HttpStatusCode.BadRequest);

            if (!await _roleManager.RoleExistsAsync(UserRoles.Moderator)) throw new HttpException(ErrorMessages.RoleNotFound, HttpStatusCode.NotFound);

            await _userManager.RemoveFromRoleAsync(user, UserRoles.Moderator);
        }
    }
}
