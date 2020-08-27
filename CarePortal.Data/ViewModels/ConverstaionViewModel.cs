using System;
using System.ComponentModel.DataAnnotations;

namespace CarePortal.Data.ViewModels
{
    public class ConversationViewModel
    {
        [Key]
        public int ConversationId { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Question { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }
        public int Category { get; set; }
        public bool IsDelete { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
    public class AddConversationViewModel
    {
        public int ConversationId { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }
        public int Category { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
    public class GetConversationViewModel
    {
        public int QuestionId { get; set; }
    }
}
