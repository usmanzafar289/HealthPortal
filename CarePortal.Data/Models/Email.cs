using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarePortal.Data.Models
{
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmailId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string PatientId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string DoctorId { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Subject { get; set; }

        [Column(TypeName = "text")]
        public string Body { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int EmailType { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsRead { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsDelete { get; set; }

        [Required]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset Timestamp { get; set; }
    }
}
