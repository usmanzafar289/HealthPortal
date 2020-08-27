using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Linq;
using CarePortal.Web.Extensions;
using Newtonsoft.Json;
using CarePortal.Data.Models;
using CarePortal.Data.Response;
using CarePortal.Data.ViewModels;
using Microsoft.Extensions.Logging;

namespace CarePortal.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger _logger;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel.Id = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                userModel.Role = HttpContext.Session.GetObject(StorageType.Role).ToString();//LocalStorageExtensions.Get(StorageType.Role);
                string response = await APICallerExtensions.APICallAsync("Search/Index", userModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return View();
                }
                var content = JsonConvert.DeserializeObject<ListResponse<SearchViewModel>>(response);
                if (!content.DidError)
                {
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
        public async Task<JsonResult> SearchDoctors(string searchText)
        {
            try
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    searchText = string.Empty;
                }
                SearchViewModel searchViewModel = new SearchViewModel();
                searchViewModel.SearchText = searchText;
                string response = await APICallerExtensions.APICallAsync("Search/SearchDoctors", searchViewModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return Json(new
                    {
                        isSuccessfull = false,
                        totalRecords = 0,
                        errorMessage = response,
                        doctorsList = string.Empty
                    });
                }
                var content = JsonConvert.DeserializeObject<ListResponse<SearchViewModel>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        isSuccessfull = true,
                        totalRecords = content.Model.Count(),
                        errorMessage = response,
                        doctorsList = content.Model
                    });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, content.Message);
                    return Json(new
                    {
                        isSuccessfull = false,
                        totalRecords = 0,
                        errorMessage = content.ErrorMessage,
                        doctorsList = string.Empty
                    });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Json(new
                {
                    isSuccessfull = false,
                    totalRecords = 0,
                    errorMessage = ex.Message,
                    doctorsList = string.Empty
                });
            }
        }
    }
}