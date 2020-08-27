using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CarePortal.Data.Models;
using CarePortal.Web.Extensions;
using Newtonsoft.Json;
using CarePortal.Data.Response;
using System.Collections.Generic;
using CarePortal.Data.ViewModels;

namespace CarePortal.Web.Controllers
{
    public class EmailController : Controller
    {
        private readonly ILogger _logger;

        public EmailController(ILogger<EmailController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetEmails(EmailType emailType)
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel.Id = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                userModel.Role = HttpContext.Session.GetObject(StorageType.Role).ToString();//LocalStorageExtensions.Get(StorageType.Role);

                //string role = LocalStorageExtensions.Get(StorageType.Role);
                userModel.Email = emailType.MailType;

                string response = await APICallerExtensions.APICallAsync("Email/GetEmails", userModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    return Json(new
                    {
                        result = false,
                        error = response
                    });
                }

                var content = JsonConvert.DeserializeObject<SingleResponse<List<EmailViewModel>>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        emails = content.Model,
                        userRole = userModel.Role,
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
        public async Task<JsonResult> EmailReply(EmailViewModel model)
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel.Id = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                userModel.Role = HttpContext.Session.GetObject(StorageType.Role).ToString();//LocalStorageExtensions.Get(StorageType.Role);

                if (userModel.Role == "Doctor")
                {
                    model.EmailType = 2;
                }
                else
                {
                    model.EmailType = 1;
                }
                model.IsRead = true;

                string response = await APICallerExtensions.APICallAsync("Email/EmailReply", model, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    return Json(new
                    {
                        result = false,
                        error = response
                    });
                }

                var content = JsonConvert.DeserializeObject<SingleResponse<EmailViewModel>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        result = true,
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
        public async Task<JsonResult> SendEmail(EmailViewModel model)
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel.Id = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                userModel.Role = HttpContext.Session.GetObject(StorageType.Role).ToString();//LocalStorageExtensions.Get(StorageType.Role);

                model.PatientId = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                model.IsRead = false;
              
                string response = await APICallerExtensions.APICallAsync("Email/EmailReply", model, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    return Json(new
                    {
                        result = false,
                        error = response
                    });
                }

                var content = JsonConvert.DeserializeObject<SingleResponse<EmailViewModel>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        result = true,
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
        public async Task<JsonResult> ReadEmail(EmailViewModel model)
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel.Id = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                userModel.Role = HttpContext.Session.GetObject(StorageType.Role).ToString();//LocalStorageExtensions.Get(StorageType.Role);

                string response = await APICallerExtensions.APICallAsync("Email/EmailRead", model, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    return Json(new
                    {
                        result = false,
                        error = response
                    });
                }

                var content = JsonConvert.DeserializeObject<SingleResponse<EmailViewModel>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        result = true,
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
        public async Task<JsonResult> DeleteEmail(EmailViewModel model)
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel.Id = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                userModel.Role = HttpContext.Session.GetObject(StorageType.Role).ToString();//LocalStorageExtensions.Get(StorageType.Role);

                string response = await APICallerExtensions.APICallAsync("Email/EmailDelete", model, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    return Json(new
                    {
                        result = false,
                        error = response
                    });
                }

                var content = JsonConvert.DeserializeObject<SingleResponse<EmailViewModel>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        result = true,
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
            catch (Exception ex)
            {
                return Json(new
                {
                    result = false,
                    error = ex.Message
                });
            }          
        }

        [HttpGet]
        public async Task<JsonResult> GetDoctors()
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel.Id = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);

                string response = await APICallerExtensions.APICallAsync("Email/GetDoctors", userModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    return Json(new
                    {
                        result = false,
                        error = response
                    });
                }

                var content = JsonConvert.DeserializeObject<SingleResponse<QuestionPageModel>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        result = true,
                        doctors = content.Model
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
    }
}