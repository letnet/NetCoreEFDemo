﻿using Microsoft.AspNetCore.Mvc;
using NetCoreEFDemo.Application;
using NetCoreEFDemo.Web.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NetCoreEFDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        ITestService _testService { get; set; }
        public HomeController(ITestService testService)
        {
            this._testService = testService;
        }

        public async Task<IActionResult> Index()
        {
           var testDto = await  _testService.Get("68f4469d-1222-4414-bf73-6823815cdb15");
            return View(testDto);
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

