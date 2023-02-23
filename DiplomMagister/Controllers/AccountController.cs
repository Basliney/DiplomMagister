using DiplomMagister.Data;
using DiplomMagister.Models;
using DiplomMagister.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiplomMagister.Controllers
{
    public class AccountController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly AccountService _accountService;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
            _accountService = new AccountService(_context);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var identity = _accountService.GetIdentity(loginViewModel.Login, loginViewModel.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            _accountService.Login(identity, HttpContext);
            
            return RedirectToActionPermanent("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            try
            {
                _accountService.Register(registerViewModel, HttpContext).Wait();
            }
            catch(UnauthorizedAccessException ex)
            {
                _accountService.Log($"Register exception {ex.ToString()}");
                return View();
            }
            catch(Exception ex)
            {
                _accountService.Log($"Unknown error {ex.ToString()}");
                return View();
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
