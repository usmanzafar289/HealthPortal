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
using System.Text;

namespace CarePortal.API.Controllers
{
    [Route("[controller]/[action]")]
    public class ConversationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConversationRepository _conversationRepository;

        public ConversationController(UserManager<ApplicationUser> userManager, ILogger<ConversationController> logger,
            IConversationRepository conversationRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _conversationRepository = conversationRepository;
        }

        [HttpPost]
        public async Task<object> GetQuestions([FromBody] UserModel userModel)
        {
            try
            {
                bool result = true;
                string error = string.Empty;

                List<ConversationViewModel> listConversationViewModel = new List<ConversationViewModel>();

                try
                {
                    var UserId = userModel.Id;

                    var user = await _userManager.FindByIdAsync(UserId);

                    bool IsDoctor = await _userManager.IsInRoleAsync(user, "Doctor");

                    listConversationViewModel = _conversationRepository.GetQuestionsByUserId(UserId, IsDoctor);
                }
                catch (Exception ex)
                {
                    result = false;
                    error = ex.Message;
                }

                return new ListResponse<ConversationViewModel>
                {
                    Message = error,
                    DidError = !result,
                    ErrorMessage = error,
                    Token = string.Empty,
                    Model = listConversationViewModel,
                };
            }
            catch (Exception ex)
            {
                return new ListResponse<ConversationViewModel>
                {
                    Message = ex.Message,
                    DidError = true,
                    ErrorMessage = ex.InnerException.ToString(),
                    Token = string.Empty,
                    Model = new List<ConversationViewModel>()
                };
            }
        }

        [HttpPost]
        public async Task<object> AddConversation([FromBody] AddConversationViewModel addConversationViewModel)
        {
            try
            {
                bool result = true;
                string error = string.Empty;

                ConversationViewModel conversation = new ConversationViewModel();
                addConversationViewModel.Timestamp = DateTimeOffset.Now;

                try
                {
                    conversation = _conversationRepository.AddConversation(addConversationViewModel.ConversationId, addConversationViewModel.DoctorId, addConversationViewModel.PatientId, string.Empty, addConversationViewModel.Message, addConversationViewModel.MessageType, addConversationViewModel.Category, false, addConversationViewModel.Timestamp);
                }
                catch (Exception ex)
                {
                    result = false;
                    error = ex.Message;
                }

                return new SingleResponse<ConversationViewModel>
                {
                    Message = error,
                    DidError = !result,
                    ErrorMessage = error,
                    Token = string.Empty,
                    Model = new ConversationViewModel()
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<ConversationViewModel>
                {
                    Message = ex.Message,
                    DidError = true,
                    ErrorMessage = ex.InnerException.ToString(),
                    Token = string.Empty,
                    Model = new ConversationViewModel()
                };
            }
        }

        [HttpPost]
        public async Task<object> GetConversation([FromBody] GetConversationViewModel getConversationViewModel)
        {
            try
            {
                bool result = true;
                string error = string.Empty;

                List<ConversationViewModel> listConversation = new List<ConversationViewModel>();
                List<ConversationViewModel> listDecodedConversation = new List<ConversationViewModel>();
                try
                {
                    listConversation = _conversationRepository.GetConversation(getConversationViewModel.QuestionId);


                    foreach (ConversationViewModel item in listConversation)
                    {
                        ConversationViewModel newItem = item;
                        byte[] data = Convert.FromBase64String(item.Message);
                        string decodedString = Encoding.UTF8.GetString(data);
                        newItem.Message = decodedString;
                        listDecodedConversation.Add(newItem);
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    error = ex.Message;
                }
                return new ListResponse<ConversationViewModel>
                {
                    Message = error,
                    DidError = !result,
                    ErrorMessage = error,
                    Token = string.Empty,
                    Model = listDecodedConversation,
                };
            }
            catch (Exception ex)
            {
                return new ListResponse<ConversationViewModel>
                {
                    Message = ex.Message,
                    DidError = true,
                    ErrorMessage = ex.InnerException.ToString(),
                    Token = string.Empty,
                    Model = new List<ConversationViewModel>()
                };
            }
        }
    }
}