using Domain.DTOs.CustomerDTOs.Requests;
using Domain.DTOs.CustomerDTOs.Responses;
using Domain.Entities.Customers;
using Domain.Exceptions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Quotas;
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
        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly ICustomerQuotasService _quotasService;
        public AccountService(UserManager<Customer> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IConfiguration configuration,
                              IStringLocalizer<ErrorMessages> localizer,
                              ICustomerQuotasService quotasService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _quotasService = quotasService;

            _localizer = localizer;
        }

        public async Task<TokenDTO> LogIn(CustomerLogInRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserBadEmail], HttpStatusCode.NotFound);

            if (!await _userManager.CheckPasswordAsync(user, model.Password)) throw new HttpException(_localizer[ErrorMessagePatterns.UserBadPassword], HttpStatusCode.NotFound);

            return new TokenDTO()
            {
                Token = await CreateTokenAsync(user)
            };
        }
        public async Task<TokenDTO> SignUp(CustomerSignUpRequest request)
        {
            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists != null)
                throw new HttpException(_localizer[ErrorMessagePatterns.UserAlreadyExists], HttpStatusCode.NotFound);

            await UserNameValidation(request.UserName);

            Customer user = new Customer()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new HttpException(_localizer[ErrorMessagePatterns.UserCreationFailed], HttpStatusCode.InternalServerError);

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            await _quotasService.AddQuotasPackToCustomer(new DTOs.QuotaDTOs.CustomerQuotaDTOs.Requests.AddQuotasToCustomerRequest()
            {
                CustomerId = user.Id,
                PackId = 1
            });

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
            var jwtConfig = _configuration.GetSection("JwtOptions");
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
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtOptions");
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
                if (!allowedCharacters.Contains(character)) throw new HttpException(_localizer[ErrorMessagePatterns.UserNameNotValid], HttpStatusCode.BadRequest);
            }
        }
    }
}
