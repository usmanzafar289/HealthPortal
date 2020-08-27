using CarePortal.API.Authentication;
using CarePortal.API.Helpers;
using CarePortal.Data.Models;
using CarePortal.Data.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarePortal.API.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost]
        public async Task<object> Login([FromBody] Login model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                var userRole = string.Empty;

                if (result.Succeeded)
                {
                    var user = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                    var roles = await _userManager.GetRolesAsync(user);
                    userRole = roles.FirstOrDefault();

                    UserModel userModel = new UserModel();
                    userModel = userModel.Set(user, userRole);

                    //If doctor is not approved by admin
                    if (userRole == "Doctor" && userModel.IsApproved != 1)
                    {
                        return new SingleResponse<ApplicationUser>
                        {
                            Message = "Doctor is not approved by the admin",
                            DidError = true,
                            ErrorMessage = string.Empty,
                            Token = string.Empty,
                            Model = null
                        };
                    }

                    //Generate token for user logged-in
                    string token = string.Empty;
                    var identity = await GetClaimsIdentity(model.Email, model.Password);
                    if (identity != null)
                    {
                        token = await Tokens.GenerateJwt(identity, _jwtFactory, model.Email, userRole, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
                    }

                    return new SingleResponse<UserModel>
                    {
                        Message = "User logged-in successfully",
                        DidError = false,
                        ErrorMessage = string.Empty,
                        Token = token,
                        Model = userModel
                    };
                }
                else
                {
                    return new SingleResponse<ApplicationUser>
                    {
                        Message = "User not found",
                        DidError = true,
                        ErrorMessage = string.Empty,
                        Token = string.Empty,
                        Model = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new SingleResponse<ApplicationUser>
                {
                    Message = string.Empty,
                    DidError = true,
                    ErrorMessage = ex.Message,
                    Token = string.Empty,
                    Model = null
                };
            }
        }

        [HttpPost]
        public async Task<object> Register([FromBody] Register model)
        {
            try
            {
                int isApproved = 1;
                if (model.UserRole == "Doctor")
                {
                    isApproved = 0;
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsApproved = isApproved
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.UserRole);

                    return new SingleResponse<ApplicationUser>
                    {
                        Message = "User created successfully",
                        DidError = false,
                        ErrorMessage = string.Empty,
                        Token = string.Empty,
                        Model = user
                    };
                }
                else
                {
                    return new SingleResponse<ApplicationUser>
                    {
                        Message = result.Errors.FirstOrDefault().Description,
                        DidError = true,
                        ErrorMessage = string.Empty,
                        Token = string.Empty,
                        Model = user
                    };
                }
            }
            catch (Exception ex)
            {
                return new SingleResponse<ApplicationUser>
                {
                    Message = string.Empty,
                    DidError = true,
                    ErrorMessage = ex.Message,
                    Token = string.Empty,
                    Model = null
                };
            }
        }

        [HttpPost]
        public async Task<object> GetRoles()
        {
            try
            {
                List<string> roles = _roleManager.Roles.Where(r => !((r.Name.Contains("Admin")))).Select(x => x.Name).ToList();

                return new SingleResponse<List<string>>
                {
                    Message = "Get roles",
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = roles
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<List<string>>
                {
                    Message = string.Empty,
                    DidError = true,
                    ErrorMessage = ex.Message,
                    Token = string.Empty,
                    Model = null
                };
            }
        }

        [HttpGet]
        public async Task<object> GetAllRoles()
        {
            try
            {
                List<string> roles = _roleManager.Roles.Select(x => x.Name).ToList();

                return new SingleResponse<List<string>>
                {
                    Message = "Get all roles",
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = roles
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<List<string>>
                {
                    Message = string.Empty,
                    DidError = true,
                    ErrorMessage = ex.Message,
                    Token = string.Empty,
                    Model = null
                };
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<object> Protected()
        {
            return "Protected area";
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return await System.Threading.Tasks.Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByEmailAsync(email);
            if (userToVerify == null) return await System.Threading.Tasks.Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await System.Threading.Tasks.Task.FromResult(_jwtFactory.GenerateClaimsIdentity(email, userToVerify.Id));
            }
            // Credentials are invalid, or account doesn't exist
            return await System.Threading.Tasks.Task.FromResult<ClaimsIdentity>(null);
        }

    }
}