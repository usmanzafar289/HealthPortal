using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarePortal.API.Repositories;
using CarePortal.Data.Models;
using CarePortal.Data.Response;
using CarePortal.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CarePortal.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailRepository _emailRepository;

        public EmailController(ILogger<FeedController> logger, UserManager<ApplicationUser> userManager, IEmailRepository emailRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _emailRepository = emailRepository;
        }

        [HttpPost]
        public async Task<object> GetEmails([FromBody] UserModel userModel)
        {
            bool result = true;
            string error = string.Empty;

            List<EmailViewModel> emailViewModel = new List<EmailViewModel>();

            try
            {
                emailViewModel = _emailRepository.GetEmails(userModel.Id, userModel.Role, userModel.Email);
            }
            catch (Exception ex)
            {
                result = false;
                error = ex.Message;
            }

            return new SingleResponse<List<EmailViewModel>>
            {
                Message = error,
                DidError = !result,
                ErrorMessage = error,
                Token = string.Empty,
                Model = emailViewModel,
            };
        }

        [HttpPost]
        public async Task<object> EmailReply([FromBody] EmailViewModel emailViewModel)
        {
            bool result = true;
            string error = string.Empty;

            EmailViewModel email = new EmailViewModel();

            try
            {
                email = _emailRepository.EmailReply(emailViewModel.PatientId, emailViewModel.DoctorId, emailViewModel.Subject, emailViewModel.Body, emailViewModel.EmailType, emailViewModel.IsRead);
            }
            catch (Exception ex)
            {
                result = false;
                error = ex.Message;
            }

            return new SingleResponse<EmailViewModel>
            {
                Message = error,
                DidError = !result,
                ErrorMessage = error,
                Token = string.Empty,
                Model = email,
            };
        }

        [HttpPost]
        public async Task<object> EmailRead([FromBody] EmailViewModel emailViewModel)
        {
            bool result = true;
            string error = string.Empty;

            EmailViewModel email = new EmailViewModel();

            try
            {
                email = _emailRepository.EmailRead(emailViewModel.EmailId);
            }
            catch (Exception ex)
            {
                result = false;
                error = ex.Message;
            }

            return new SingleResponse<EmailViewModel>
            {
                Message = error,
                DidError = !result,
                ErrorMessage = error,
                Token = string.Empty,
                Model = email,
            };
        }

        [HttpPost]
        public async Task<object> EmailDelete([FromBody] EmailViewModel emailViewModel)
        {
            bool result = true;
            string error = string.Empty;

            EmailViewModel email = new EmailViewModel();

            try
            {
                email = _emailRepository.EmailDelete(emailViewModel.EmailId);
            }
            catch (Exception ex)
            {
                result = false;
                error = ex.Message;
            }

            return new SingleResponse<EmailViewModel>
            {
                Message = error,
                DidError = !result,
                ErrorMessage = error,
                Token = string.Empty,
                Model = email,
            };
        }

        [HttpPost]
        public async Task<object> GetDoctors([FromBody] UserModel userModel)
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
                    Message = "",
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
    }
}