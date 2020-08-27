using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarePortal.Data.Models;
using CarePortal.API.Repositories;
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

namespace CarePortal.API.Controllers
{
    [Route("[controller]/[action]")]
    //[ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileRepository _profileRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public ProfileController(UserManager<ApplicationUser> userManager, IProfileRepository profileRepository,
            IDepartmentRepository departmentRepository)
        {
            _userManager = userManager;
            _profileRepository = profileRepository;
            _departmentRepository = departmentRepository;
        }

        // GET: Profile
        [HttpPost]
        public async Task<object> Index([FromBody] ProfileViewModel webProfileViewModel)
        {
            ProfileViewModel profileModel = new ProfileViewModel();

            ////var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(webProfileViewModel.UserId);

            profileModel.UserId = user.Id;
            profileModel.UserName = string.Concat(user.FirstName, " ", user.LastName);
            profileModel.Picture = user.Picture;
            profileModel.Description = user.Description;

            profileModel.listDepartment = new List<Department>();
            profileModel.listDepartment = _profileRepository.GetAllDepartments();

            profileModel.UserSelectedDepartments = new List<Department>();
            profileModel.UserSelectedDepartments = _departmentRepository.GetDepartmentsByUserId(user.Id);
            profileModel.UserSelectedDepartmentsIds = new int[profileModel.UserSelectedDepartments.Count];
            int count = 0;
            foreach (var item in profileModel.UserSelectedDepartments)
            {
                profileModel.UserSelectedDepartmentsIds[count] = item.DepartmentId;
                count++;
            }

            return new SingleResponse<ProfileViewModel>
            {
                Message = "User info fetched into model",
                DidError = false,
                ErrorMessage = string.Empty,
                Token = string.Empty,
                Model = profileModel
            };
        }

        [HttpPost]
        public async Task<object> Create([FromBody] ProfileViewModel profileModel)
        {
            var user = await _userManager.FindByIdAsync(profileModel.UserId);

            if (!string.IsNullOrEmpty(profileModel.Description))
            {
                user.Description = profileModel.Description;
            }
            if (!string.IsNullOrEmpty(profileModel.base64Picture))
            {
                user.Picture = profileModel.base64Picture;
            }

            var result = await _userManager.UpdateAsync(user);

            DateTimeOffset timestamp = DateTimeOffset.Now;
            if (profileModel.UserSelectedDepartmentsIds != null)
            {
                List<UserDepartment> userDepartments = _profileRepository.AddUserDepartments(profileModel.UserId, string.Join(",", profileModel.UserSelectedDepartmentsIds), timestamp);
            }
            //return RedirectToAction("Index");
            return new SingleResponse<ApplicationUser>
            {
                Message = "Profile Updated",
                DidError = false,
                ErrorMessage = string.Empty,
                Token = string.Empty,
                Model = null
            };
        }
    }
}