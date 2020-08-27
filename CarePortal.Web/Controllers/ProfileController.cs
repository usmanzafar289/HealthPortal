using CarePortal.Data.Models;
using CarePortal.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CarePortal.Web.Extensions;
using Newtonsoft.Json;
using CarePortal.Data.Response;
using Microsoft.Extensions.Logging;

namespace CarePortal.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger _logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }

        // GET: Profile
        public async Task<ActionResult> Index(string userId)
        {
            try
            {
                bool canUpdate = false;
                if (string.IsNullOrEmpty(userId))
                {
                    canUpdate = true;
                    userId = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                }
                
                ProfileViewModel profileViewModel = new ProfileViewModel();
                
                profileViewModel.UserId = userId;
                string response = await APICallerExtensions.APICallAsync("Profile/Index", profileViewModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return View();
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<ProfileViewModel>>(response);
                if (!content.DidError)
                {
                    content.Model.canUpdate = canUpdate;
                    return View(content.Model);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, content.Message);
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProfileViewModel profileModel)
        {
            try
            {
                string userId = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                profileModel.UserId = userId;
                string response = await APICallerExtensions.APICallAsync("Profile/Create", profileModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return View(profileModel);
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<ApplicationUser>>(response);
                if (!content.DidError)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, content.Message);
                    return View(profileModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
                return View(profileModel);
            }
        }
    }
}