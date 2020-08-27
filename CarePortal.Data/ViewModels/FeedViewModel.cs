using CarePortal.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarePortal.Data.ViewModels
{
    public class FeedViewModel
    {
        [Key]
        public int FeedId { get; set; }
        public string Data { get; set; }
        public bool IsDelete { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string UserId { get; set; }
        public int DepartmentId { get; set; }
        public string Picture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public List<CommentsViewModel> CommentsViewModel { get; set; }
    }

    public class FeedPageModel
    {
        
        public List<FeedViewModel> listFeed { get; set; }
        public List<Department> listDepartment { get; set; }
        public List<SelectListItem> listDepartmentItems { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPicture { get; set; }
        public string UserRole { get; set; }

        public int PageNumber{ get; set; }
        public int PageSize { get; set; }
    }
    public class AddFeedResponse
    {
        public string UserId { get; set; }
        public int FeedId { get; set; }
        public int Response { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
