using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CarePortal.Web.Extensions;
using Newtonsoft.Json;
using CarePortal.Data.Models;
using CarePortal.Data.Response;
using CarePortal.Data.ViewModels;
using Microsoft.Extensions.Logging;

namespace CarePortal.Web.Controllers
{
    public class ConversationController : Controller
    {
        private readonly ILogger _logger;

        public ConversationController(ILogger<ConversationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetQuestions()
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel.Id = HttpContext.Session.GetObject(StorageType.UserId).ToString();// LocalStorageExtensions.Get(StorageType.UserId);
                string response = await APICallerExtensions.APICallAsync("Conversation/GetQuestions", userModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return Json(new
                    {
                        isSuccessfull = false,
                        errorMessage = response
                    });
                }
                var content = JsonConvert.DeserializeObject<ListResponse<ConversationViewModel>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        isSuccessfull = true,
                        listConversationViewModel =content.Model,
                    });
                }
                else
                {
                    return Json(new
                    {
                        isSuccessfull = false,
                        errorMessage = content.Message
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    isSuccessfull = false,
                    errorMessage = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddConversation(int conversationId, string doctorId, string patientId,
            string message, int messageType, int category)
        {
            try
            {
                AddConversationViewModel addConversationViewModel = new AddConversationViewModel();
                addConversationViewModel.ConversationId = conversationId;
                addConversationViewModel.DoctorId = doctorId;
                addConversationViewModel.PatientId = patientId;
                addConversationViewModel.Message = message;
                addConversationViewModel.MessageType = messageType;
                addConversationViewModel.Category = category;
                string response = await APICallerExtensions.APICallAsync("Conversation/AddConversation", addConversationViewModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return Json(new
                    {
                        isSuccessfull = false,
                        errorMessage = response
                    });
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<ConversationViewModel>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        isSuccessfull = true,
                        conversation = content.Model,
                    });
                }
                else
                {
                    return Json(new
                    {
                        isSuccessfull = false,
                        errorMessage = content.Message
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    isSuccessfull = false,
                    errorMessage = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetConversation(int questionId)
        {
            try
            {

                GetConversationViewModel getConversationViewModel = new GetConversationViewModel();
                getConversationViewModel.QuestionId = questionId;
                string response = await APICallerExtensions.APICallAsync("Conversation/GetConversation", getConversationViewModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return Json(new
                    {
                        isSuccessfull = false,
                        errorMessage = response
                    });
                }
                var content = JsonConvert.DeserializeObject<ListResponse<ConversationViewModel>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        isSuccessfull = true,
                        listDecodedConversation = content.Model,
                    });
                }
                else
                {
                    return Json(new
                    {
                        isSuccessfull = false,
                        errorMessage = content.Message
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    isSuccessfull = false,
                    errorMessage = ex.Message
                });
            }
        }
    }

}