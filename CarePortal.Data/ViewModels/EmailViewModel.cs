using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarePortal.Data.ViewModels
{
    public class EmailViewModel
    {
        [Key]
        public int EmailId { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int EmailType { get; set; }
        public bool IsRead { get; set; }
        public bool IsDelete { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string EmailAddress { get; set; }
        public string Picture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UnreadMail { get; set; }
    }

    public class EmailVM
    {
        public string Group { get; set; }
        public List<EmailViewModel> List { get; set; }
    }

    public class EmailType
    {
        public string MailType { get; set; }
    }
}
