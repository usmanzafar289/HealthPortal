using CarePortal.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarePortal.Data.ViewModels
{
    public class CalendarViewModel
    {
        public string UserId { get; set; }
        public string UserRole { get; set; }
        public List<SelectListItem> listDoctorsItems { get; set; }
        public List<ApplicationUserListViewModel> listPatients { get; set; }
        public List<CalendarEventModel> listEvents { get; set; }
    }
    public class CalendarEventModel
    {
        [Key]
        public int CalendarId { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public int Status { get; set; }
        public bool Success { get; set; }
        public bool IsDelete { get; set; }
        public int Type { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
    //public class AddEventViewModel
    //{
    //    public string UserId { get; set; }
    //    public CalendarEventModel calendarEventModel { get; set; }
    //}
}
