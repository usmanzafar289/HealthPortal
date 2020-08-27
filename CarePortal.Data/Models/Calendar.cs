using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarePortal.Data.Models
{
    public class Calendar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CalendarId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string PatientId { get; set; }
        public ApplicationUser ApplicationUser_Patient { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string DoctorId { get; set; }
        public ApplicationUser ApplicationUser_Doctor { get; set; }

        [Required]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset StartTime { get; set; }

        [Required]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset EndTime { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Duration { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Type { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Status { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Notes { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool Success { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsDelete { get; set; }

        [Required]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset Timestamp { get; set; }
    }
}
