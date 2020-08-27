using CarePortal.API.Repositories;
using CarePortal.Data.Models;
using CarePortal.Data.Response;
using CarePortal.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarePortal.API.Controllers
{
    [Route("[controller]/[action]")]
    //[ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICalendarRepository _calendarRepository;

        public CalendarController(UserManager<ApplicationUser> userManager, ILogger<CalendarController> logger,
            ICalendarRepository calendarRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _calendarRepository = calendarRepository;
        }

        [HttpPost]
        public async Task<object> GetDoctors([FromBody] CalendarViewModel model)
        {
            try
            {
                //Get all doctors
                var list = new List<ApplicationUserListViewModel>();
                var usersOfRole = await _userManager.GetUsersInRoleAsync("Doctor");
                foreach (var users in usersOfRole)
                {
                    list.Add(new ApplicationUserListViewModel()
                    {
                        UserEmail = users.Email,
                        IsApproved = users.IsApproved,
                        UserId = users.Id,
                        FirstName = users.FirstName,
                        LastName = users.LastName
                    });
                }
                model.listDoctorsItems = new List<SelectListItem>();
                foreach (var item in list)
                {
                    SelectListItem listItem = new SelectListItem
                    {
                        Text = string.Concat(item.FirstName, " ", item.LastName),
                        Value = item.UserId
                    };
                    model.listDoctorsItems.Add(listItem);
                }

                //Get all patients
                var listPatients = new List<ApplicationUserListViewModel>();
                var userPatients = await _userManager.GetUsersInRoleAsync("Patient");
                foreach (var patient in userPatients)
                {
                    listPatients.Add(new ApplicationUserListViewModel()
                    {
                        UserEmail = patient.Email,
                        IsApproved = patient.IsApproved,
                        UserId = patient.Id,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName
                    });
                }
                model.listPatients = new List<ApplicationUserListViewModel>();
                model.listPatients = listPatients;

                return new SingleResponse<CalendarViewModel>
                {
                    Message = string.Empty,
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = model
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<CalendarViewModel>
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
        public async Task<object> GetEvents([FromBody] CalendarViewModel model)
        {
            try
            {
                model.listEvents = new List<CalendarEventModel>();
                model.listEvents = _calendarRepository.GetEvents(model.UserId);

                return new SingleResponse<CalendarViewModel>
                {
                    Message = string.Empty,
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = model
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<CalendarViewModel>
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
        public object AddEvent([FromBody] CalendarEventModel calendarEventModel)
        {
            try
            {
                try
                {
                    Calendar calendar = _calendarRepository.AddEvent(calendarEventModel.DoctorId, calendarEventModel.PatientId,
                            calendarEventModel.StartTime, calendarEventModel.EndTime, calendarEventModel.Title, calendarEventModel.Notes,
                            calendarEventModel.Duration, calendarEventModel.Status, 1, true, false, DateTimeOffset.Now);

                    calendarEventModel.CalendarId = calendar.CalendarId;

                    return new SingleResponse<CalendarEventModel>
                    {
                        Message = string.Empty,
                        DidError = false,
                        ErrorMessage = string.Empty,
                        Token = string.Empty,
                        Model = calendarEventModel
                    };
                }
                catch (Exception ex)
                {
                    return new SingleResponse<CalendarEventModel>
                    {
                        Message = string.Empty,
                        DidError = true,
                        ErrorMessage = ex.Message,
                        Token = string.Empty,
                        Model = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new SingleResponse<CalendarEventModel>
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
        public object UpdateEvent([FromBody] CalendarEventModel calendarEventModel)
        {
            try
            {
                Calendar calendar = _calendarRepository.UpdateEvent(calendarEventModel.CalendarId,
                        calendarEventModel.StartTime, calendarEventModel.EndTime, calendarEventModel.Title, calendarEventModel.Notes,
                        calendarEventModel.Duration, calendarEventModel.Status, 1, true, false, DateTimeOffset.Now);

                return new SingleResponse<CalendarEventModel>
                {
                    Message = string.Empty,
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = calendarEventModel
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<CalendarEventModel>
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
        public object DeleteEvent([FromBody] CalendarEventModel calendarEventModel)
        {
            try
            {
                Calendar calendar = _calendarRepository.DeleteEvent(calendarEventModel.CalendarId,
                        calendarEventModel.IsDelete, DateTimeOffset.Now);

                return new SingleResponse<CalendarEventModel>
                {
                    Message = string.Empty,
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = calendarEventModel
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<CalendarEventModel>
                {
                    Message = string.Empty,
                    DidError = true,
                    ErrorMessage = ex.Message,
                    Token = string.Empty,
                    Model = null
                };
            }
        }

        //[HttpPost]
        //public object UpdateCalendarEvent([FromBody] CalendarEventModel values)
        //{
        //    try
        //    {
        //        //if (values.ListCalendarEventModel.Count > 0)
        //        //{
        //        //    List<Calendar> calendarList = new List<Calendar>();

        //        //    foreach (var item in values.ListCalendarEventModel)
        //        //    {
        //        //        Calendar calendar = new Calendar();
        //        //        DateTimeOffset timestamp = DateTimeOffset.Now;

        //        //        var patentId = values.UserId;

        //        //        try
        //        //        {
        //        //            calendar = _calendarRepository.UpdateCalendarEvent(item.EventId,item.DoctorId, patentId, item.AppointmentTime, item.Note, item.Duration, 0, 1, true, false, timestamp);
        //        //            calendarList.Add(calendar);
        //        //        }
        //        //        catch (Exception ex)
        //        //        {
        //        //            return new SingleResponse<CalendarViewModel>
        //        //            {
        //        //                Message = string.Empty,
        //        //                DidError = true,
        //        //                ErrorMessage = ex.Message,
        //        //                Token = string.Empty,
        //        //                Model = null
        //        //            };
        //        //        }
        //        //    }

        //        //    return new SingleResponse<List<Calendar>>
        //        //    {
        //        //        Message = string.Empty,
        //        //        DidError = false,
        //        //        ErrorMessage = string.Empty,
        //        //        Token = string.Empty,
        //        //        Model = calendarList
        //        //    };

        //        //    //return Json(new
        //        //    //{
        //        //    //    calendarList,
        //        //    //    result,
        //        //    //    error
        //        //    //});
        //        //}
        //        //else
        //        //{
        //        //    return new SingleResponse<List<Calendar>>
        //        //    {
        //        //        Message = "Zero Events Received",
        //        //        DidError = false,
        //        //        ErrorMessage = "Zero Events Received",
        //        //        Token = string.Empty,
        //        //        Model = null
        //        //    };

        //        //    //bool result = false;
        //        //    //string error = "Zero Events Received";
        //        //    //return Json(new
        //        //    //{
        //        //    //    result,
        //        //    //    error
        //        //    //});
        //        //}
        //        return new SingleResponse<CalendarViewModel>
        //        {
        //            Message = string.Empty,
        //            DidError = true,
        //            ErrorMessage = string.Empty,
        //            Token = string.Empty,
        //            Model = null
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new SingleResponse<CalendarViewModel>
        //        {
        //            Message = string.Empty,
        //            DidError = true,
        //            ErrorMessage = ex.Message,
        //            Token = string.Empty,
        //            Model = null
        //        };
        //    }
        //}
    }
}
