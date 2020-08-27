using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarePortal.Data.Models
{
    public class FeedResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedResponseId { get; set; }

        [Required]
        [ForeignKey("Feed")]
        public int FeedId { get; set; }
        public Feed Feed { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Response { get; set; }

        [Required]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset Timestamp { get; set; }
    }
}
