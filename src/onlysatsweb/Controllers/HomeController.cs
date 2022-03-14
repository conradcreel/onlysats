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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _Client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _Client = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            var response = await _Client.GetAsync("https://google.com");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();
            var vm = new HomeModel
            {
                DataSize = data.Length
            };

            return View(vm);
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
