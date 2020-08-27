using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarePortal.Data.Models
{
    public class UserDepartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserDepartmentId { get; set; }

        [Required]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset Timestamp { get; set; }
    }
}
