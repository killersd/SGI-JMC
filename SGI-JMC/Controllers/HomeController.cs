using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SGI_JMC.Extensions;
using SGI_JMC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SGI_JMC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            BasicNotification("Notificação",NotificationType.Success,"Notificação");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

 
    }
}
