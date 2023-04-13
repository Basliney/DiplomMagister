using DiplomMagister.Classes.Client;
using DiplomMagister.Classes.StaticData;
using DiplomMagister.Models;
using DiplomMagister.Services.OutServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DiplomMagister.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [Authorize(Roles = "Пользователь")]
        public async Task<IActionResult> Index()
        {
            await _emailService.SendEmailAsync("somemail@mail.ru", "Тема письма", "Тест письма: тест!");
            Program.Logger.Log(NLog.LogLevel.Warn, "User is here");
            return View();
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