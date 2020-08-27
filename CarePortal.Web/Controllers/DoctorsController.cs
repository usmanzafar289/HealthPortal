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
    public class DoctorsController : Controller
    {
        private readonly ILogger _logger;

        public DoctorsController(ILogger<DoctorsController> logger)
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
                string response = await APICallerExtensions.APICallAsync("Doctors/Index", userModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return View();
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<DoctorsViewModel>>(response);
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
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(DoctorsViewModel model)
        {
            try
            {
                string response = await APICallerExtensions.APICallAsync("Doctors/UpdateStatus", model, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return RedirectToAction(nameof(Index));
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<DoctorsViewModel>>(response);
                if (!content.DidError)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, content.Message);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        
        // GET: Doctors/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctors/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: Doctors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Doctors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<JsonResult> UpdateIsApprovedBit(string emailId, int isApproved)
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel.Email = emailId;
                userModel.IsApproved = isApproved;
                string response = await APICallerExtensions.APICallAsync("Doctors/UpdateIsApprovedBit", userModel, false, HttpContext.Session.GetObject(StorageType.Token).ToString());
                if (response.ToLower().Contains("exception:"))
                {
                    ModelState.AddModelError(string.Empty, response);
                    return Json(new
                    {
                        isSuccessfull = false,
                        errorMessage = response
                    });
                }
                var content = JsonConvert.DeserializeObject<SingleResponse<DoctorsViewModel>>(response);
                if (!content.DidError)
                {
                    return Json(new
                    {
                        isSuccessfull = true,
                        errorMessage = string.Empty
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
            catch (System.Exception ex)
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