using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarePortal.Data.Models;
using CarePortal.API.Repositories;
using CarePortal.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using CarePortal.API.Authentication;
using CarePortal.API.Helpers;
using CarePortal.Data.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace CarePortal.API.Controllers
{
    [Route("[controller]/[action]")]
    public class FeedController : ControllerBase
    {
        private readonly ILogger _logger;
        private UserManager<ApplicationUser> _userManager;
        private readonly IFeedRepository _feedRepository;
        private readonly IFeedResponseRepository _feedResponseRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public FeedController(ILogger<FeedController> logger, UserManager<ApplicationUser> userManager,
            IFeedRepository feedRepository, IDepartmentRepository departmentRepository, IFeedResponseRepository feedResponseRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _feedRepository = feedRepository;
            _feedResponseRepository = feedResponseRepository;
            _departmentRepository = departmentRepository;
        }

        [HttpPost]
        public async Task<object> Index([FromBody] UserModel userModel)
        {
            try
            {
                FeedPageModel model = new FeedPageModel();
                var id = userModel.Id;
                //adding picture instead of using cookies
                model.UserPicture = userModel.Picture;
                //if (Request.Cookies["Picture"] != null)
                //{
                //    model.UserPicture = Request.Cookies["Picture"];
                //    if (string.IsNullOrEmpty(model.UserPicture))
                //    {
                //        model.UserPicture = "user.jpg";
                //    }
                //}
                model.PageNumber = 0;
                model.PageSize = 5;

                model.listFeed = new List<FeedViewModel>();
                model.listFeed = _feedRepository.GetFeedById(id, model.PageNumber, model.PageSize);
                List<FeedViewModel> listFeed = new List<FeedViewModel>();
                foreach (FeedViewModel item in model.listFeed)
                {
                    FeedViewModel newItem = item;
                    byte[] data = Convert.FromBase64String(item.Data);
                    string decodedString = Encoding.UTF8.GetString(data);
                    newItem.Data = decodedString;

                    //adding comments of the feed
                    newItem.CommentsViewModel = new List<CommentsViewModel>();
                    newItem.CommentsViewModel = _feedRepository.GetCommentsByFeedId(item.FeedId);

                    listFeed.Add(newItem);
                }
                model.listFeed = listFeed;

                model.listDepartment = _departmentRepository.GetDepartmentsByUserId(id);

                model.listDepartmentItems = new List<SelectListItem>();
                foreach (var item in model.listDepartment)
                {
                    SelectListItem listItem = new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.DepartmentId.ToString()
                    };
                    model.listDepartmentItems.Add(listItem);
                }

                return new SingleResponse<FeedPageModel>
                {
                    Message = "Feed fetched successfully",
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = model
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<FeedPageModel>
                {
                    Message = ex.InnerException.ToString(),
                    DidError = true,
                    ErrorMessage = ex.Message,
                    Token = string.Empty,
                    Model = null
                };
            }
        }

        [HttpPost]
        public async Task<object> AddFeed([FromBody] FeedViewModel webFeedViewModel)
        {
            try
            {
                bool result = true;
                string error = string.Empty;

                Feed feed = new Feed();

                var id = webFeedViewModel.UserId;
                DateTimeOffset timestamp = DateTimeOffset.Now;
                try
                {
                    feed = _feedRepository.AddFeed(id, webFeedViewModel.Data, false, timestamp, webFeedViewModel.DepartmentId);

                    byte[] byteArray = Convert.FromBase64String(webFeedViewModel.Data);
                    string decodedString = Encoding.UTF8.GetString(byteArray);
                    feed.Data = decodedString;
                }
                catch (Exception ex)
                {
                    result = false;
                    error = ex.Message;
                }

                return new SingleResponse<Feed>
                {
                    Message = "Added to feed successfully",
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = feed
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<Feed>
                {
                    Message = ex.InnerException.ToString(),
                    DidError = true,
                    ErrorMessage = ex.Message,
                    Token = string.Empty,
                    Model = null
                };
            }
        }
        [HttpPost]
        public async Task<object> LoadMore([FromBody] FeedPageModel webFeedPageModel)
        {
            FeedPageModel model = new FeedPageModel();

            bool result = true;
            string error = string.Empty;

            try
            {
                var id = webFeedPageModel.UserId;

                model.PageNumber = webFeedPageModel.PageNumber + 1;
                model.PageSize = webFeedPageModel.PageSize;

                model.listFeed = new List<FeedViewModel>();
                model.listFeed = _feedRepository.GetFeedById(id, model.PageNumber, model.PageSize);
                List<FeedViewModel> listFeed = new List<FeedViewModel>();
                foreach (FeedViewModel item in model.listFeed)
                {
                    FeedViewModel newItem = item;
                    byte[] data = Convert.FromBase64String(item.Data);
                    string decodedString = Encoding.UTF8.GetString(data);
                    newItem.Data = decodedString;
                    //adding comments of the feed
                    newItem.CommentsViewModel = new List<CommentsViewModel>();
                    newItem.CommentsViewModel = _feedRepository.GetCommentsByFeedId(item.FeedId);

                    listFeed.Add(newItem);
                }
                model.listFeed = listFeed;

                model.listDepartment = _departmentRepository.GetDepartmentsByUserId(id);

                model.listDepartmentItems = new List<SelectListItem>();
                foreach (var item in model.listDepartment)
                {
                    SelectListItem listItem = new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.DepartmentId.ToString()
                    };
                    model.listDepartmentItems.Add(listItem);
                }

            }
            catch (Exception ex)
            {
                result = false;
                error = ex.Message;
            }

            return new SingleResponse<FeedPageModel>
            {
                Message = error,
                DidError = !result,
                ErrorMessage = error,
                Token = string.Empty,
                Model = model
            };
        }

        [HttpPost]
        public async Task<object> AddFeedResponse([FromBody] AddFeedResponse addFeedResponse)
        {
            bool result = true;
            string error = string.Empty;

            FeedResponse feedResponse = new FeedResponse();

            var id = addFeedResponse.UserId;
            DateTimeOffset timestamp = DateTimeOffset.Now;
            try
            {
                feedResponse = _feedResponseRepository.AddFeedResponse(id, addFeedResponse.FeedId, addFeedResponse.Response, timestamp);
            }
            catch (Exception ex)
            {
                result = false;
                error = ex.Message;
            }

            return new SingleResponse<FeedResponse>
            {
                Message = error,
                DidError = !result,
                ErrorMessage = error,
                Token = string.Empty,
                Model = feedResponse
            };
        }
        [HttpPost]
        public object AddComment([FromBody] CommentsViewModel webCommentsViewModel)
        {
            try
            {

                var id = webCommentsViewModel.UserId;

                Comment commentResponse = new Comment();
                DateTimeOffset timestamp = DateTimeOffset.Now;
                commentResponse = _feedResponseRepository.AddComment(id, webCommentsViewModel.FeedId, webCommentsViewModel.Comments, timestamp);


                return new SingleResponse<Comment>
                {
                    Message = "Comment Added Successfully",
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = commentResponse
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<Comment>{
                    Message = ex.InnerException.ToString(),
                    DidError = true,
                    ErrorMessage = ex.Message,
                    Token = string.Empty,
                    Model = null
                };
            }
        }
    }
}