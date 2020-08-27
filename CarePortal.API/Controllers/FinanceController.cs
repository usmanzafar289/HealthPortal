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

namespace CarePortal.API.Controllers
{
    [Route("[controller]/[action]")]
    //[ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFinanceRepository _financeRepository;

        public FinanceController(
            ILogger<FinanceController> logger, UserManager<ApplicationUser> userManager, IFinanceRepository financeRepository)
        {
            _logger = logger;
            _financeRepository = financeRepository;
            _userManager = userManager;
        }

        [HttpPost]
        public object Index([FromBody] UserModel userModel)
        {
            try
            {
                FinanceViewModel financeViewModel = new FinanceViewModel();
                var userId = userModel.Id;
                financeViewModel.Subscriptions = new List<Subscription>();
                financeViewModel.Subscriptions = _financeRepository.GetSubscriptions(userId);
                financeViewModel.PaymentMethods = new List<PaymentMethod>();
                financeViewModel.PaymentMethods = _financeRepository.GetPaymentMethods(userId);
                return new SingleResponse<FinanceViewModel>
                {
                    Message = string.Empty,
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = financeViewModel
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<FinanceViewModel>
                {
                    Message = ex.Message,
                    DidError = true,
                    ErrorMessage = ex.InnerException.ToString(),
                    Token = string.Empty,
                    Model = new FinanceViewModel()
                };
            }
        }
        [HttpPost]
        public object AddPaymentMethod([FromBody] AddPaymentViewModel addPaymentViewModel)
        {
            try
            {
                _financeRepository.AddPaymentMethod(addPaymentViewModel);
                return new SingleResponse<PaymentMethod>
                {
                    Message = "Payment Method added successfully",
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = new PaymentMethod()
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<PaymentMethod>
                {
                    Message = ex.Message,
                    DidError = true,
                    ErrorMessage = ex.InnerException.ToString(),
                    Token = string.Empty,
                    Model = new PaymentMethod()
                };
            }
        }

        [HttpPost]
        public object AddBalance([FromBody] AddBalanceViewModel addBalanceViewModel)
        {
            try
            {
                addBalanceViewModel.TransactionId = "fake-id";
                addBalanceViewModel.TransactionDate = DateTimeOffset.Now;
                addBalanceViewModel.Timestamp = DateTimeOffset.Now;
                Subscription subscriptionItem = new Subscription();
                subscriptionItem = _financeRepository.AddBalance(addBalanceViewModel.UserId, addBalanceViewModel.Amount, addBalanceViewModel.TransactionId, addBalanceViewModel.Timestamp, addBalanceViewModel.TransactionDate);
                return new SingleResponse<Subscription>
                {
                    Message = "Balance added successfully",
                    DidError = false,
                    ErrorMessage = string.Empty,
                    Token = string.Empty,
                    Model = subscriptionItem
                };
            }
            catch (Exception ex)
            {
                return new SingleResponse<Subscription>
                {
                    Message = ex.Message,
                    DidError = true,
                    ErrorMessage = ex.InnerException.ToString(),
                    Token = string.Empty,
                    Model = new Subscription()
                };
            }
        }
    }
}