using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarePortal.Data.ViewModels
{
    public class QuestionPageModel
    {
        public List<SelectListItem> listDoctorsItems { get; set; }
    }
    public class AddQuestionViewModel
    {
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
