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
using Microsoft.Extensions.Logging;

namespace CarePortal.API.Controllers
{
    [Route("[controller]/[action]")]
    //[Authorize(Roles = "Patient")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public SearchController(ILogger<SearchController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<object> Index(UserModel userModel)
        {
            try
            {
                var usersOfRole = await _userManager.GetUsersInRoleAsync("Doctor");
                List<SearchViewModel> searchViewModels = new List<SearchViewModel>();
                foreach (var item in usersOfRole)
                {
                    SearchViewModel searchViewModel = new SearchViewModel();
                    searchViewModel.UserId = item.Id;
                    searchViewModel.EmailId = item.Email;
                    searchViewModel.FirstName = item.FirstName;
                    searchViewModel.LastName = item.LastName;
                    searchViewModel.ImageURL = item.Picture;
                    searchViewModels.Add(searchViewModel);
                }
                return new ListResponse<SearchViewModel>
                {
                    Message = "All doctors fectched successfully",
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = searchViewModels
                };
            }
            catch (Exception ex)
            {
                return new ListResponse<SearchViewModel>
                {
                    Message = ex.InnerException.ToString(),
                    DidError = true,
                    ErrorMessage = ex.Message,
                    Token = string.Empty,
                    Model = null
                };
            }
        }

        [HttpPost]
        public async Task<object> SearchDoctors([FromBody] SearchViewModel webSearchViewModel)
        {
            try
            {
                var usersOfRole = await _userManager.GetUsersInRoleAsync("Doctor");
                var result = usersOfRole.Where(s => (s.FirstName + " " + s.LastName).ToLower().Contains(webSearchViewModel.SearchText.ToLower()));
                List<SearchViewModel> searchViewModels = new List<SearchViewModel>();
                foreach (var item in result)
                {
                    SearchViewModel searchViewModel = new SearchViewModel();
                    searchViewModel.UserId = item.Id;
                    searchViewModel.EmailId = item.Email;
                    searchViewModel.FirstName = item.FirstName;
                    searchViewModel.LastName = item.LastName;
                    searchViewModel.ImageURL = item.Picture;
                    searchViewModels.Add(searchViewModel);
                }
                return new ListResponse<SearchViewModel>
                {
                    Message = "search results fetched successfully",
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = searchViewModels
                };
            }
            catch (Exception ex)
            {
                return new ListResponse<SearchViewModel>
                {
                    Message = "Error while searching",
                    DidError = false,
                    ErrorMessage = ex.Message,
                    Token = string.Empty,
                    Model = null
                };
            }
        }
    }
}