using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarePortal.Data.Models;
using CarePortal.Data.ViewModels;
using CarePortal.Web.Extensions;
using Newtonsoft.Json;
using CarePortal.Data.Response;
using Microsoft.Extensions.Logging;

namespace CarePortal.Web.Controllers
{
    public class FinanceController : Controller
    {
        private readonly ILogger _logger;

        public FinanceController(ILogger<FinanceController> logger)
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
                string response = await APICallerExtensions.APICallAsync("Finance/Index", userModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return View();
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<FinanceViewModel>>(response);
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
        public async Task<IActionResult> AddPaymentMethod(IFormCollection paymentMethodFormData)
        {
            try
            {
                AddPaymentViewModel addPaymentViewModel = new AddPaymentViewModel();
                string month = String.Empty;
                string year = String.Empty;
                addPaymentViewModel.UserId = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                foreach (string key in paymentMethodFormData.Keys)
                {
                    if (key == "fname")
                    {
                        addPaymentViewModel.FirstName = paymentMethodFormData[key];
                    }
                    if (key == "lname")
                    {
                        addPaymentViewModel.LastName = paymentMethodFormData[key];
                    }
                    if (key == "cnumber")
                    {
                        addPaymentViewModel.CardNumber = paymentMethodFormData[key];
                    }
                    if (key == "month")
                    {
                        month = paymentMethodFormData[key];
                    }
                    if (key == "year")
                    {
                        year = paymentMethodFormData[key];
                    }
                    if (key == "cvv")
                    {
                        addPaymentViewModel.CVV = Convert.ToInt32(paymentMethodFormData[key]);
                    }
                    if (key == "is_default")
                    {
                        addPaymentViewModel.IsDefault = Convert.ToBoolean((Convert.ToByte(paymentMethodFormData[key])));
                    }
                }
                addPaymentViewModel.Expiry = month + year;
                addPaymentViewModel.Timestamp = DateTimeOffset.Now;
                addPaymentViewModel.IsDelete = false;

                string response = await APICallerExtensions.APICallAsync("Finance/AddPaymentMethod", addPaymentViewModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return RedirectToAction("Index");
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<FinanceViewModel>>(response);
                if (!content.DidError)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, content.Message);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBalance(IFormCollection addBalanceFormData)
        {
            try
            {
                AddBalanceViewModel addBalanceViewModel = new AddBalanceViewModel();
                addBalanceViewModel.UserId = HttpContext.Session.GetObject(StorageType.UserId).ToString();//LocalStorageExtensions.Get(StorageType.UserId);
                foreach (var key in addBalanceFormData.Keys)
                {
                    if (key == "amount")
                    {
                        addBalanceViewModel.Amount = Convert.ToDecimal(addBalanceFormData[key]);
                    }
                }

                string response = await APICallerExtensions.APICallAsync("Finance/AddBalance", addBalanceViewModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return RedirectToAction("Index");
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<Subscription>>(response);
                if (!content.DidError)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, content.Message);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}