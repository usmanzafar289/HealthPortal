using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Twilio.Jwt.AccessToken;
using System.Collections.Generic;
using System;
using CarePortal.Web.Extensions;

namespace CarePortal.Web.Controllers
{
    public class VideoChatController : Controller
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public VideoChatController(ILogger<VideoChatController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetToken()
        {
            // Load Twilio configuration from Web.config
            var accountSid = _configuration["TwilioSettings:AccountSid"];
            var apiKey = _configuration["TwilioSettings:ApiKey"];
            var apiSecret = _configuration["TwilioSettings:ApiSecret"];

            // Create a random identity for the client
            var identity = HttpContext.Session.GetObject(StorageType.Name).ToString();// LocalStorageExtensions.Get(StorageType.Name);

            // Create a video grant for the token
            var grant = new VideoGrant();
            grant.Room = "Care Portal - " + String.Format("{0:ddd, MMM d, yyyy}", DateTime.Now);
            var grants = new HashSet<IGrant> { grant };

            // Create an Access Token generator
            var token = new Token(accountSid, apiKey, apiSecret, identity: identity, grants: grants);

            return Json(new
            {
                identity = identity,
                token = token.ToJwt()
            });
        }

    }
}