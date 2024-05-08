using Domain.DTOs.CustomerDTOs.Requests;
using Domain.DTOs.CustomerDTOs.Responses;
using Domain.Entities.Customers;
using Domain.Exceptions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Other;
using Domain.Interfaces.Quotas;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Domain.Templates;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TL;
using static OpenAI.ObjectModels.SharedModels.IOpenAiModels;

namespace Domain.Services.Customers
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRepository<Customer> _customersRepository;

        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly ICustomerQuotasService _quotasService;
        private readonly IMyEmailSender _emailSender;
        private readonly IUserService _userService;
        public AccountService(UserManager<Customer> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IConfiguration configuration,
                              IStringLocalizer<ErrorMessages> localizer,
                              ICustomerQuotasService quotasService,
                              IMyEmailSender emailSender,
                              IUserService userService,
                              IRepository<Customer> customersRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _quotasService = quotasService;
            _emailSender = emailSender;
            _userService = userService;

            _localizer = localizer;
            _customersRepository = customersRepository;
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

            if (!await _roleManager.RoleExistsAsync(UserRoles.Visitor))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Visitor));

            if (await _roleManager.RoleExistsAsync(UserRoles.Visitor))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Visitor);
            }

            await SendEmailConfirmation(user);

            return await LogIn(new CustomerLogInRequest()
            {
                Email = request.Email,
                Password = request.Password
            });
        }
        public async Task<TokenDTO> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotFound], HttpStatusCode.NotFound);

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.EmailConfirmationError], HttpStatusCode.BadRequest);
            }

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

            return new TokenDTO()
            {
                Token = await CreateTokenAsync(user)
            };
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

        public async Task<bool> CheckEmailConfirmation()
        {
            var id = await _userService.GetCurrentUserId();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotFound], HttpStatusCode.NotFound);

            return user.EmailConfirmed;
        }
        public async Task SendEmailConfirmation()
        {
            var id = await _userService.GetCurrentUserId();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotFound], HttpStatusCode.NotFound);

            await SendEmailConfirmation(user);
        }
        private async Task SendEmailConfirmation(Customer user)
        {
            if (user.LastEmailConfirmationSend != null)
            {
                TimeSpan difference = DateTime.UtcNow-(DateTime)user.LastEmailConfirmationSend;

                if (difference.TotalMinutes < 1)
                {
                    throw new HttpException(_localizer[ErrorMessagePatterns.EmailSendingToFast], HttpStatusCode.BadRequest);
                }
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var link = _configuration["SiteLink"] + "#/main-layout/clusterization/workspaces/list(overflow:confirm-email)?token=" + HttpUtility.UrlEncode(token) + "&amp;" + "email=" + HttpUtility.UrlEncode(user.Email);
            var body = PopulateBody(link);
            await _emailSender.SendEmail("Email Confirmation", body, user.UserName, user.Email);

            user.LastEmailConfirmationSend = DateTime.UtcNow;
            await _customersRepository.SaveChangesAsync();
        }
        private string PopulateBody(string link)
        {
            string body = "<!DOCTYPE html><html> <head> <title>Email confirmation</title> </head> <body style=\"margin: 0; font-family: Roboto, 'Helvetica Neue', sans-serif; background-color: #f5f6f8;\"> <div style=\"margin: 20px 0;\"> <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout: fixed; margin: 0 auto;\"> <tr style=\"height:20px\"></tr> <tr> <td align=\"center\"> <table cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: white; border-radius: 8px; padding: 20px; box-sizing: border-box;\"> <tr> <td> <h1 style=\"color: #414a5f; text-align: center;\">Confirm Your Evoclust Account</h1> <p style=\"color: #676f81;\"> Thanks for signing up for an account on Evoclust! To start watching, please confirm your email address below so we know you're you... </p> <p style=\"margin-bottom: 20px;\"> <a href=\"{link}\" style=\"text-decoration: none; color: #ffffff; background-color: #58637a; padding: 10px 20px; border-radius: 5px; display: inline-block;\">CONFIRM EMAIL ADDRESS</a> </p> <p style=\"color: #676f81;\"> You can also copy and paste the following link into your browser to confirm your email: <br> <a href=\"{link}\" style=\"color: #007bff; text-decoration: none;\">{link}</a> </p> <p style=\"color: #676f81;\"> If you did not sign up for an account on Evoclust and believe someone registered this email by mistake, please contact us so we can resolve this issue. </p> </td> </tr> </table> </td> </tr> <tr style=\"height:20px\"></tr> </table> </div> </body></html>";
            body = body.Replace("{link}", link);
            return body;
        }
    }
}
