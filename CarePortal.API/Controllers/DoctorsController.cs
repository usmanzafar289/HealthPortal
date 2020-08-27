using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarePortal.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using CarePortal.API.Authentication;
using CarePortal.API.Helpers;
using CarePortal.Data.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Claims;
using CarePortal.Data.Models;


namespace CarePortal.API.Controllers
{
    [Route("[controller]/[action]")]
    //[ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DoctorsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        //[HttpGet]
        [HttpPost]
        public async Task<object> Index(UserModel userModel)
        {
            try
            {
                var list = new List<ApplicationUserListViewModel>();
                foreach (var user in _userManager.Users.ToList())
                {
                    list.Add(new ApplicationUserListViewModel()
                    {
                        UserEmail = user.Email,
                        Roles = await _userManager.GetRolesAsync(user),
                        IsApproved = user.IsApproved
                    });
                }
                DoctorsViewModel model = new DoctorsViewModel();
                model.UserList = new List<ApplicationUserListViewModel>();
                foreach (var user in list)
                {
                    foreach (var role in user.Roles)
                    {
                        if (role == "Doctor")
                        {
                            model.UserList.Add(user);
                        }
                    }
                }
                //return View(model);
                return new SingleResponse<DoctorsViewModel>
                {
                    Message = "User info fetched into model",
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = model
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<DoctorsViewModel>
                {
                    Message = ex.Message,
                    DidError = true,
                    ErrorMessage = ex.InnerException.ToString(),
                    Token = string.Empty,
                    Model = new DoctorsViewModel()
                };
            }
        }

        [HttpPost]
        public async Task<object> UpdateStatus([FromBody] DoctorsViewModel model)
        {
            try
            {
                var user = new ApplicationUser();
                user = await _userManager.FindByEmailAsync(model.Email);

                user.IsApproved = model.Status;
                var result = await _userManager.UpdateAsync(user);
                return new SingleResponse<object>
                {
                    Message = "Status Updated",
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = null
                };
                //return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                //return View();
                return new SingleResponse<object>
                {
                    Message = "Status Update Failed",
                    DidError = true,
                    ErrorMessage = ex.Message,
                    Token = string.Empty,
                    Model = null
                };
            }
        }


        //public async Task<IActionResult> Approve(string email)
        //{
        //    var user = new ApplicationUser();
        //    user = await _userManager.FindByEmailAsync(email);

        //    user.IsApproved = 1;
        //    var result = await _userManager.UpdateAsync(user);

        //    return View("Index");
        //}
        //public async Task<IActionResult> Reject(string email)
        //{
        //    var user = new ApplicationUser();
        //    user = await _userManager.FindByEmailAsync(email);

        //    user.IsApproved = 2;
        //    var result = await _userManager.UpdateAsync(user);

        //    return View("Index");
        //}


        [HttpPost]
        public async Task<object> UpdateIsApprovedBit([FromBody] UserModel userModel)
        {
            var user = new ApplicationUser();
            user = await _userManager.FindByEmailAsync(userModel.Email);

            user.IsApproved = userModel.IsApproved;
            var result = await _userManager.UpdateAsync(user);
            return new SingleResponse<object>
            {
                Message = result.Errors.ToString(),
                DidError = result.Succeeded,
                ErrorMessage = result.Errors.ToString(),
                Token = string.Empty,
                Model = null
            };
        }
    }
}