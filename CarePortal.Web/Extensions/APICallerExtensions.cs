using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CarePortal.Web.Extensions
{
    public static class APICallerExtensions
    {
        public static IHostingEnvironment _hostingEnvironment { get; set; }
        public static IConfiguration _configuration { get; set; }

        public static async System.Threading.Tasks.Task<string> APICallAsync(string apiUrl, object model, bool isAuthorized, string token)
        {
            string response = string.Empty;
            try
            {
                var client = new HttpClient();
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                string baseUrl = string.Empty;

                if (_hostingEnvironment.IsDevelopment())
                {
                    baseUrl = _configuration["ApiSettings:ApiUrlDev"];
                }
                else
                {
                    baseUrl = _configuration["ApiSettings:ApiUrlProd"];
                }
                if (isAuthorized)
                {
                    string authorization = "Bearer " + token;//LocalStorageExtensions.Get(StorageType.Token);
                    httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(baseUrl + apiUrl),
                        Headers = {
                            { HttpRequestHeader.Authorization.ToString(), authorization },
                        },
                        Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
                    };
                }
                else
                {
                    httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(baseUrl + apiUrl),
                        Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
                    };
                }
                HttpResponseMessage responseMessage = client.SendAsync(httpRequestMessage).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    response = await responseMessage.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                response = "Exception: " + ex.Message;
            }
            return response;
        }

    }
}
