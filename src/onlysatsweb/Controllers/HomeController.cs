using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using onlysats.web.Models;
using System.Net.Http;

namespace onlysats.web.Controllers
{
    public class HomeController : _BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _Client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            _Client = new HttpClient(handler);
        }

        public async Task<IActionResult> Index(int x = 0)
        {
            if (x > 0)
            {
                try
                {
                    var response = await _Client.GetAsync("https://synapse.embassy:8448/_matrix/client/r0/publicRooms?access_token=syt_YWRtaW4_SWbYUHIsJFCZvdPmCbqG_01oq9A");
                    response.EnsureSuccessStatusCode();

                    var data = await response.Content.ReadAsStringAsync();
                    var vm = new HomeModel
                    {
                        DataSize = data.Length
                    };


                    return View(vm);
                }
                catch (Exception ex)
                {
                    return View(new HomeModel { DataSize = -1 });
                }
            }
            else
            {
                return View(new HomeModel());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
