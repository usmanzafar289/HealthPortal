using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarePortal.Data.Models
{
    public class PaymentMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentMethodId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string FirstName { get; set; }

        [Column(TypeName = "text")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string CardNumber { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Expiry { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int CVV { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsDefault { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsDelete { get; set; }

        [Required]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset Timestamp { get; set; }
    }
}
