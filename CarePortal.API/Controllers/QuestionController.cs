using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarePortal.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using CarePortal.API.Repositories;
using CarePortal.API.Authentication;
using CarePortal.API.Helpers;
using CarePortal.Data.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Claims;
using CarePortal.Data.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace CarePortal.API.Controllers
{
    [Route("[controller]/[action]")]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConversationRepository _conversationRepository;

        public QuestionController(ILogger<FeedController> logger, UserManager<ApplicationUser> userManager,
            IConversationRepository conversationRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _conversationRepository = conversationRepository;
        }

        [HttpPost]
        public async Task<object> Index([FromBody] UserModel userModel)
        {
            try
            {
                var list = new List<ApplicationUserListViewModel>();

                var usersOfRole = await _userManager.GetUsersInRoleAsync("Doctor");

                foreach (var user in usersOfRole)
                {
                    list.Add(new ApplicationUserListViewModel()
                    {
                        UserEmail = user.Email,
                        IsApproved = user.IsApproved,
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    });
                }
                QuestionPageModel model = new QuestionPageModel();
                model.listDoctorsItems = new List<SelectListItem>();
                foreach (var item in list)
                {
                    SelectListItem listItem = new SelectListItem
                    {
                        Text = string.Concat(item.FirstName, " ", item.LastName),
                        Value = item.UserId
                    };
                    model.listDoctorsItems.Add(listItem);
                }

                return new SingleResponse<QuestionPageModel>
                {
                    Message = "Questions fetched",
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = model
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<QuestionPageModel>
                {
                    Message = ex.Message,
                    DidError = true,
                    ErrorMessage = ex.InnerException.ToString(),
                    Token = string.Empty,
                    Model = new QuestionPageModel()
                };
            }
        }

        [HttpPost]
        public async Task<object> GetDoctorInformation([FromBody] UserModel userModel)
        {
            bool result = true;
            string error = string.Empty;
            ApplicationUser user = null;
            try
            {
                user = await _userManager.FindByIdAsync(userModel.Id);
            }
            catch (Exception ex)
            {
                result = false;
                error = ex.Message;
            }

            return new SingleResponse<ApplicationUser>
            {
                Message = error,
                DidError = !result,
                ErrorMessage = error,
                Token = string.Empty,
                Model=user,
            };
        }

        [HttpPost]
        public async Task<object> AddQuestion([FromBody] AddQuestionViewModel addQuestionViewModel)
        {
            bool result = true;
            string error = string.Empty;

            Conversation conversation = new Conversation();
            addQuestionViewModel.Timestamp = DateTimeOffset.Now;

            try
            {
                conversation = _conversationRepository.AddQuestion(addQuestionViewModel.DoctorId, addQuestionViewModel.PatientId, addQuestionViewModel.Title, addQuestionViewModel.Message, 1, 0, false, addQuestionViewModel.Timestamp);
            }
            catch (Exception ex)
            {
                result = false;
                error = ex.Message;
            }

            return new SingleResponse<Conversation>
            {
                Message = error,
                DidError = !result,
                ErrorMessage = error,
                Token = string.Empty,
                Model = conversation,
            };
        }

    }
}