using CarePortal.Data.Models;
using CarePortal.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CarePortal.Web.Extensions;
using Newtonsoft.Json;
using CarePortal.Data.Response;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CarePortal.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ILogger _logger;

        public QuestionController(ILogger<QuestionController> logger)
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

                string response = await APICallerExtensions.APICallAsync("Question/Index", userModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return View();
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<QuestionPageModel>>(response);
                if (!content.DidError)
                {
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
        public async Task<JsonResult> GetDoctorInformation(string userId)
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel.Id = userId;
                string response = await APICallerExtensions.APICallAsync("Question/GetDoctorInformation", userModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    return Json(new
                    {
                        result = false,
                        error = response
                    });
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<UserModel>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        user = content.Model,
                    });
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        error = content.Message
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
        public async Task<JsonResult> AddQuestion(string doctorId, string patientId, string title, string message)
        {
            AddQuestionViewModel addQuestionViewModel = new AddQuestionViewModel();
            addQuestionViewModel.DoctorId = doctorId;
            addQuestionViewModel.PatientId = patientId;
            addQuestionViewModel.Title = title;
            addQuestionViewModel.Message = message;

            string response = await APICallerExtensions.APICallAsync("Question/AddQuestion", addQuestionViewModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
            if (response.ToLower().Contains("exception:"))
            {
                return Json(new
                {
                    result = false,
                    error = response
                });
            }
            var content = JsonConvert.DeserializeObject<SingleResponse<Conversation>>(response);
            if (!content.DidError)
            {
                return Json(new
                {
                    result=true,
                    conversation = content.Model
                });
            }
            else
            {
                return Json(new
                {
                    result = false,
                    error = content.Message
                });
            }
        }


    }
}