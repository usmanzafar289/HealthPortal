using CarePortal.Data.Response;
using CarePortal.Data.ViewModels;
using CarePortal.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarePortal.Web.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ILogger _logger;

        public CalendarController(ILogger<CalendarController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            CalendarViewModel model = new CalendarViewModel();
            model.UserId = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
            model.UserRole = HttpContext.Session.GetObject(StorageType.Role).ToString();//LocalStorageExtensions.Get(StorageType.Role);

            string response = await APICallerExtensions.APICallAsync("Calendar/GetDoctors", model, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
            if (response.ToLower().Contains("exception:"))
            {
                ModelState.AddModelError(string.Empty, response);
                return View(model);
            }
            var content = JsonConvert.DeserializeObject<SingleResponse<CalendarViewModel>>(response);
            model = content.Model;

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetEvents()
        {
            bool result = true;
            string error = string.Empty;

            CalendarViewModel calendarViewModel = new CalendarViewModel();
            calendarViewModel.UserId = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);

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
                List<EventRoot> eventRoot = EventMapping(content.Model.listEvents);
                return Json(new
                {
                    eventRoot,
                    result,
                    error
                });
            }
            else
            {
                result = false;
                error = response;
                return Json(new
                {
                    result,
                    error
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddEvent([FromBody]CalendarEventModel clendarEventModel)
        {
            bool result = true;
            string error = string.Empty;

            CalendarEventModel calendarEventModel = new CalendarEventModel
            {
                CalendarId = 0,
                DoctorId = clendarEventModel.DoctorId,
                PatientId = HttpContext.Session.GetObject(StorageType.UserId).ToString(),//LocalStorageExtensions.Get(StorageType.UserId),
                StartTime = clendarEventModel.StartTime,
                EndTime = clendarEventModel.EndTime,
                Duration = clendarEventModel.Duration,
                Title = clendarEventModel.Title,
                Notes = clendarEventModel.Notes,
                Status = clendarEventModel.Status,
                Success = true,
                IsDelete = false,
                Type = 1,
                Timestamp = DateTimeOffset.Now,
            };

            string response = await APICallerExtensions.APICallAsync("Calendar/AddEvent", calendarEventModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
            if (string.IsNullOrEmpty(response) || response.ToLower().Contains("exception:"))
            {
                ModelState.AddModelError(string.Empty, response);
                result = false;
                error = response;
                return Json(new
                {
                    result,
                    response
                });
            }
            var content = JsonConvert.DeserializeObject<SingleResponse<CalendarEventModel>>(response);
            if (!content.DidError)
            {
                return Json(new
                {
                    content,
                    result,
                    error
                });
            }
            else
            {
                result = false;
                error = response;
                return Json(new
                {
                    result,
                    error
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateEvent([FromBody]CalendarEventModel clendarEventModel)
        {
            bool result = true;
            string error = string.Empty;

            CalendarEventModel calendarEventModel = new CalendarEventModel
            {
                CalendarId = clendarEventModel.CalendarId,
                StartTime = clendarEventModel.StartTime,
                EndTime = clendarEventModel.EndTime,
                Duration = clendarEventModel.Duration,
                Title = clendarEventModel.Title,
                Notes = clendarEventModel.Notes,
                Status = clendarEventModel.Status,
                Success = true,
                IsDelete = false,
                Type = 1,
                Timestamp = DateTimeOffset.Now,
            };

            string response = await APICallerExtensions.APICallAsync("Calendar/UpdateEvent", calendarEventModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
            if (string.IsNullOrEmpty(response) || response.ToLower().Contains("exception:"))
            {
                ModelState.AddModelError(string.Empty, response);
                result = false;
                error = response;
                return Json(new
                {
                    result,
                    response
                });
            }
            var content = JsonConvert.DeserializeObject<SingleResponse<CalendarEventModel>>(response);
            if (!content.DidError)
            {
                return Json(new
                {
                    content,
                    result,
                    error
                });
            }
            else
            {
                result = false;
                error = response;
                return Json(new
                {
                    result,
                    error
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteEvent([FromBody]CalendarEventModel clendarEventModel)
        {
            bool result = true;
            string error = string.Empty;

            CalendarEventModel calendarEventModel = new CalendarEventModel
            {
                CalendarId = clendarEventModel.CalendarId,
                IsDelete = true,
                Type = 1,
                Timestamp = DateTimeOffset.Now,
            };

            string response = await APICallerExtensions.APICallAsync("Calendar/DeleteEvent", calendarEventModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
            if (string.IsNullOrEmpty(response) || response.ToLower().Contains("exception:"))
            {
                ModelState.AddModelError(string.Empty, response);
                result = false;
                error = response;
                return Json(new
                {
                    result,
                    response
                });
            }
            var content = JsonConvert.DeserializeObject<SingleResponse<CalendarEventModel>>(response);
            if (!content.DidError)
            {
                return Json(new
                {
                    content,
                    result,
                    error
                });
            }
            else
            {
                result = false;
                error = response;
                return Json(new
                {
                    result,
                    error
                });
            }
        }

        //[HttpPost]
        //public async Task<JsonResult> UpdateCalendarEvents([FromBody]List<CalendarEventModel> values)
        //{

        //    bool result = true;
        //    string error = string.Empty;

        //    //AddEventViewModel updateEventViewModel = new AddEventViewModel();
        //    //updateEventViewModel.ListCalendarEventModel = values;
        //    //updateEventViewModel.UserId = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);

        //    //string response = await APICallerExtensions.APICallAsync("Calendar/UpdateCalendarEvent", updateEventViewModel, false);
        //    //if (response.ToLower().Contains("exception:"))
        //    //{
        //    //    ModelState.AddModelError(string.Empty, response);

        //    //    result = false;
        //    //    error = "Zero Events Received";
        //    //    return Json(new
        //    //    {
        //    //        result,
        //    //        error
        //    //    });
        //    //}

        //    //var content = JsonConvert.DeserializeObject<SingleResponse<List<Calendar>>>(response);
        //    //List<Calendar> calendarList = content.Model;

        //    //if (calendarList.Count > 0)
        //    //{
        //    //    return Json(new
        //    //    {
        //    //        calendarList,
        //    //        result,
        //    //        error
        //    //    });
        //    //}
        //    //else
        //    //{
        //    //    result = false;
        //    //    error = "Zero Events Received";
        //    //    return Json(new
        //    //    {
        //    //        result,
        //    //        error
        //    //    });
        //    //}

        //    return Json(new
        //    {
        //        result,
        //        error
        //    });
        //}

        public List<EventRoot> EventMapping(List<CalendarEventModel> listCalendar)
        {
            List<EventRoot> listEventRoot = new List<EventRoot>();

            foreach (var item in listCalendar)
            {
                EventRoot eventRoot = new EventRoot();
                eventRoot.other = new EventOther();

                eventRoot.title = item.Title;
                if (item.Status == 0)
                {
                    eventRoot.@class = "bg-warning";
                }
                else if (item.Status == 1)
                {
                    eventRoot.@class = "bg-success";
                }
                else if (item.Status == 2)
                {
                    eventRoot.@class = "bg-danger";
                }

                eventRoot.start = item.StartTime;
                eventRoot.end = item.EndTime;
                eventRoot.other.notes = item.Notes;
                eventRoot.other.doctorId = item.DoctorId;
                eventRoot.other.patientId = item.PatientId;
                eventRoot.other.calendarId = item.CalendarId;
                eventRoot.other.doctorName = item.DoctorName;
                eventRoot.other.patientName = item.PatientName;
                eventRoot.other.status = item.Status;

                listEventRoot.Add(eventRoot);
            }


            return listEventRoot;
        }
    }

    public class EventRoot
    {
        public string title { get; set; }
        public string @class { get; set; }
        public DateTimeOffset start { get; set; }
        public DateTimeOffset end { get; set; }
        public EventOther other { get; set; }
    }
    public class EventOther
    {
        public string notes { get; set; }
        public string doctorId { get; set; }
        public string patientId { get; set; }
        public int calendarId { get; set; }
        public string doctorName { get; set; }
        public string patientName { get; set; }
        public int status { get; set; }
    }
}