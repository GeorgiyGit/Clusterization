using Domain.DTOs.CustomerDTOs.Requests;
using Domain.DTOs.CustomerDTOs.Responses;
using Domain.Entities.Customers;
using Domain.Exceptions;
using Domain.Interfaces.Customers;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Customers
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Customer> userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration configuration;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        public AccountService(UserManager<Customer> userManager,
                              RoleManager<IdentityRole> _roleManager,
                              IConfiguration configuration,
                              IStringLocalizer<ErrorMessages> localizer)
        {
            this.userManager = userManager;
            this._roleManager = _roleManager;
            this.configuration = configuration;


            this.localizer = localizer;
        }

        public async Task<TokenDTO> LogIn(CustomerLogInRequest model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null) throw new HttpException(localizer[ErrorMessagePatterns.UserBadEmail], HttpStatusCode.NotFound);

            if (!await userManager.CheckPasswordAsync(user, model.Password)) throw new HttpException(localizer[ErrorMessagePatterns.UserBadPassword], HttpStatusCode.NotFound);

            return new TokenDTO()
            {
                Token = await CreateTokenAsync(user)
            };
        }
        public async Task<TokenDTO> SignUp(CustomerSignUpRequest request)
        {
            var userExists = await userManager.FindByEmailAsync(request.Email);
            if (userExists != null)
                throw new HttpException(localizer[ErrorMessagePatterns.UserAlreadyExists], HttpStatusCode.NotFound);

            await UserNameValidation(request.UserName);

            Customer user = new Customer()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
            };

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new HttpException(localizer[ErrorMessagePatterns.UserCreationFailed], HttpStatusCode.InternalServerError);

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }

            return await LogIn(new CustomerLogInRequest()
            {
                Email = request.Email,
                Password = request.Password
            });
        }
        public async Task<string> CreateTokenAsync(Customer user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = configuration.GetSection("JwtOptions");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims(Customer user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("userId",user.Id)
                };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = configuration.GetSection("JwtOptions");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(Convert.ToDouble(jwtSettings["Lifetime"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

        private async Task UserNameValidation(string userName)
        {
            string allowedCharacters = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯабвгґдеєжзиіїйклмнопрстуфхцчшщьюяĆćČčĎďĐđŁłŃńŇňŐőŘřŚśŠšŤťŽžљњћџђњћџABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿĀāĂăĄąĆćĈĉĊċČčĎďĐđĒēĔĕĖėĘęĚěĜĝĞğĠġĢģĤĥĦħĨĩĪīĬĭĮįİıĴĵĶķĹĺĻļĽľĿŀŁłŃńŅņŇňŉŌōŎŏŐőŒœŔŕŖŗŘřŚśŜŝŞşŠšŢţŤťŦŧŨũŪūŬŭŮůŰűŲųŴŵŶŷŸŹźŻżŽžǺǻǼǽǾǿȘșȚțəɐɑɒɓɔɕɖɗəɛɜɡɣɥɨɪɫɬɭɯɰɱɲɳɵɹɻɽɾʀʁʂʃʄʅʉʊʋʌʍʎʏʐʑʒʔμאבגдהוזחטיכלמנסעפצקרשתاآبتثجحخدذرزسشصضطظعغفقكلمنهوياأإآةىءصقفعظعظةلىكسمنتيكى_- ";
            foreach (var character in userName)
            {
                if (!allowedCharacters.Contains(character)) throw new HttpException(localizer[ErrorMessagePatterns.UserNameNotValid], HttpStatusCode.BadRequest);
            }
        }
    }
}
