using CarePortal.Data.Response;
using CarePortal.Data.ViewModels;
using CarePortal.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarePortal.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ILogger _logger;

        public AppointmentController(ILogger<AppointmentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            bool result = true;
            string error = string.Empty;

            CalendarViewModel calendarViewModel = new CalendarViewModel();
            calendarViewModel.UserId = HttpContext.Session.GetObject(StorageType.UserId).ToString();// LocalStorageExtensions.Get(StorageType.UserId);
            calendarViewModel.UserRole = HttpContext.Session.GetObject(StorageType.Role).ToString();// LocalStorageExtensions.Get(StorageType.Role);

            string response = await APICallerExtensions.APICallAsync("Calendar/GetEvents", calendarViewModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
            if (response.ToLower().Contains("exception:"))
            {
                ModelState.AddModelError(string.Empty, response);
                result = false;
                error = response;
                return Json(new
                {
                    result,
                    error
                });
            }
            var content = JsonConvert.DeserializeObject<SingleResponse<CalendarViewModel>>(response);
            if (!content.DidError)
            {
                calendarViewModel.listEvents = content.Model.listEvents;
            }
            return View(calendarViewModel);
        }

    }

}