using CarePortal.Data.Models;
using CarePortal.Data.Models.AccountViewModels;
using CarePortal.Data.Response;
using CarePortal.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarePortal.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        public static IHostingEnvironment _hostingEnvironment { get; set; }

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        private readonly string ApiLoginID = ConfigurationManager.AppSetting["AuthorizeNetConfig:apiLoginId"];
        private readonly string ApiTransactionKey = ConfigurationManager.AppSetting["AuthorizeNetConfig:transactionKey"];

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                string response = await APICallerExtensions.APICallAsync("Account/Login", model, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (string.IsNullOrEmpty(response) || response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return View(model);
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<UserModel>>(response);
                if (!content.DidError)
                {
                    //LocalStorageExtensions.Store(StorageType.Token, content.Token);
                    //LocalStorageExtensions.Store(StorageType.UserId, content.Model.Id);
                    //LocalStorageExtensions.Store(StorageType.Username, content.Model.UserName);

                    HttpContext.Session.SetObject(StorageType.Token, content.Token);
                    HttpContext.Session.SetObject(StorageType.UserId, content.Model.Id);
                    HttpContext.Session.SetObject(StorageType.Username, content.Model.UserName);
                    //var objComplex = HttpContext.Session.GetObject(StorageType.Token);

                    if (string.IsNullOrEmpty(content.Model.Picture))
                    {
                        if (_hostingEnvironment.IsDevelopment())
                        {
                            //LocalStorageExtensions.Store(StorageType.Picture, "/images/user.jpg");
                            HttpContext.Session.SetObject(StorageType.Picture, "/images/user.jpg");
                        }
                        else
                        {
                            //LocalStorageExtensions.Store(StorageType.Picture, "../images/user.jpg");
                            HttpContext.Session.SetObject(StorageType.Picture, "../images/user.jpg");
                        }

                    }
                    else
                    {
                        //LocalStorageExtensions.Store(StorageType.Picture, content.Model.Picture);
                        HttpContext.Session.SetObject(StorageType.Picture, content.Model.Picture);
                    }
                    //LocalStorageExtensions.Store(StorageType.Role, content.Model.Role);
                    //LocalStorageExtensions.Store(StorageType.Name, content.Model.FirstName + " " + content.Model.LastName);

                    HttpContext.Session.SetObject(StorageType.Role, content.Model.Role);
                    HttpContext.Session.SetObject(StorageType.Name, content.Model.FirstName + " " + content.Model.LastName);


                    if (content.Model.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Doctors");
                    }
                    else if (content.Model.Role == "Doctor")
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else if (content.Model.Role == "Patient")
                    {
                        return RedirectToAction("Index", "Feed");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, content.Message);
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            RegisterViewModel model = new RegisterViewModel();
            string response = await APICallerExtensions.APICallAsync("Account/GetRoles", null, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
            if (response.ToLower().Contains("exception:"))
            {
                ModelState.AddModelError(string.Empty, response);
                return View(model);
            }
            var content = JsonConvert.DeserializeObject<SingleResponse<List<string>>>(response);
            if (!content.DidError)
            {
                List<SelectListItem> listRoleItems = new List<SelectListItem>();
                model.listRoles = content.Model.ToList();
                foreach (var item in model.listRoles)
                {
                    SelectListItem listItem = new SelectListItem
                    {
                        Text = item,
                        Value = item
                    };
                    listRoleItems.Add(listItem);
                }
                model.listRoleItems = listRoleItems;
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                string response = await APICallerExtensions.APICallAsync("Account/Register", model, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    List<SelectListItem> listRoleItems = new List<SelectListItem>();
                    foreach (var item in model.listRoles)
                    {
                        SelectListItem listItem = new SelectListItem
                        {
                            Text = item,
                            Value = item
                        };
                        listRoleItems.Add(listItem);
                    }
                    model.listRoleItems = listRoleItems;
                    return View(model);
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<UserModel>>(response);
                if (!content.DidError)
                {
                    _logger.LogInformation("User created successfully.");
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, content.Message);
                    List<SelectListItem> listRoleItems = new List<SelectListItem>();
                    foreach (var item in model.listRoles)
                    {
                        SelectListItem listItem = new SelectListItem
                        {
                            Text = item,
                            Value = item
                        };
                        listRoleItems.Add(listItem);
                    }
                    model.listRoleItems = listRoleItems;
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            //LocalStorageExtensions.Clear();
            HttpContext.Session.Clear();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion

    }
}
