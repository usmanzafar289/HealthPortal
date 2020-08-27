using CarePortal.Data.Models;
using CarePortal.Data.Response;
using CarePortal.Data.ViewModels;
using CarePortal.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CarePortal.Web.Controllers
{
    public class FeedController : Controller
    {
        private readonly ILogger _logger;

        public FeedController(ILogger<FeedController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel.Id = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                userModel.Role = HttpContext.Session.GetObject(StorageType.Role).ToString();//LocalStorageExtensions.Get(StorageType.Role);
                userModel.Picture = HttpContext.Session.GetObject(StorageType.Picture).ToString();//LocalStorageExtensions.Get(StorageType.Picture);

                string response = await APICallerExtensions.APICallAsync("Feed/Index", userModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return View();
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<FeedPageModel>>(response);
                if (!content.DidError)
                {
                    //content.Model.UserName = HttpContext.Session.GetObject(StorageType.Name).ToString();//LocalStorageExtensions.Get(StorageType.Name);
                    //content.Model.UserPicture = HttpContext.Session.GetObject(StorageType.Picture).ToString();//LocalStorageExtensions.Get(StorageType.Picture);
                    content.Model.UserRole = HttpContext.Session.GetObject(StorageType.Role).ToString();//LocalStorageExtensions.Get(StorageType.Role);

                    return View(content.Model);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, content.Message);
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }


        [HttpPost]
        public async Task<JsonResult> AddFeed(string data, int departmentId)
        {
            try
            {
                FeedViewModel addFeedViewModel = new FeedViewModel();
                addFeedViewModel.UserId = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                addFeedViewModel.Data = data;
                addFeedViewModel.DepartmentId = departmentId;
                string response = await APICallerExtensions.APICallAsync("Feed/AddFeed", addFeedViewModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return Json(new
                    {
                        result = false,
                        error = response
                    });
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<Feed>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        content.Model,
                    });
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        error = content.ErrorMessage
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    result = false,
                    error = ex.Message
                });
            }
        }

        public async Task<JsonResult> LoadMore(int PageNumber, int PageSize)
        {
            try
            {
                FeedPageModel loadMoreFeedPageModel = new FeedPageModel();
                loadMoreFeedPageModel.UserId = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                loadMoreFeedPageModel.PageNumber = PageNumber;
                loadMoreFeedPageModel.PageSize = PageSize;
                string response = await APICallerExtensions.APICallAsync("Feed/LoadMore", loadMoreFeedPageModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return Json(new
                    {
                        result = false,
                        error = response
                    });
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<FeedPageModel>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        content.Model,
                    });
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        error = content.ErrorMessage
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    result = false,
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddFeedResponse(int feedId, int feedResponse)
        {
            try
            {
                AddFeedResponse addFeedResponse = new AddFeedResponse();
                addFeedResponse.UserId = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                addFeedResponse.FeedId = feedId;
                addFeedResponse.Response = feedResponse;
                string response = await APICallerExtensions.APICallAsync("Feed/AddFeedResponse", addFeedResponse, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return Json(new
                    {
                        result = false,
                        error = response
                    });
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<FeedResponse>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        content.Model,
                    });
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        error = content.ErrorMessage
                    });
                }
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    result = false,
                    error = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddComment(int feedId, string newComment)
        {
            try
            {
                CommentsViewModel addCommentViewModel = new CommentsViewModel();
                addCommentViewModel.UserId = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);

                addCommentViewModel.FeedId = feedId;
                addCommentViewModel.Comments = newComment;

                var userName = HttpContext.Session.GetObject(StorageType.Name).ToString();//LocalStorageExtensions.Get(StorageType.Name);
                var imageURL = HttpContext.Session.GetObject(StorageType.Picture).ToString();//LocalStorageExtensions.Get(StorageType.Picture);

                string response = await APICallerExtensions.APICallAsync("Feed/AddComment", addCommentViewModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return Json(new
                    {
                        result = false,
                        error = response
                    });
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<Comment>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        content.Model,
                        userName,
                        imageURL
                    });
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        error = content.ErrorMessage
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
    }
}