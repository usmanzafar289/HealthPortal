using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarePortal.Data.Models
{
    public class Conversation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConversationId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string PatientId { get; set; }
        public ApplicationUser ApplicationUser_Patient { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string DoctorId { get; set; }
        public ApplicationUser ApplicationUser_Doctor { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int QuestionID { get; set; }

        [Column(TypeName = "text")]
        public string Question { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Message { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int MessageType { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Category { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsDelete { get; set; }

        [Required]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset Timestamp { get; set; }
    }
}
