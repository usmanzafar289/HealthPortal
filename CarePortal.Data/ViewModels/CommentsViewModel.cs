using System;
using System.ComponentModel.DataAnnotations;

namespace CarePortal.Data.ViewModels
{
    public class CommentsViewModel
    {
        [Key]
        public int CommentId { get; set; }
        public string Comments { get; set; }
        public string UserName { get; set; }
        public string ProfilePic { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public int FeedId { get; set; }
        public string UserId { get; set; }
    }
}
