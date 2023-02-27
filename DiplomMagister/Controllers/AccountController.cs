using DiplomMagister.Data;
using DiplomMagister.Models;
using DiplomMagister.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToActionPermanent("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                var identity = _accountService.GetIdentity(loginViewModel.Login, loginViewModel.Password);
                if (identity == null)
                {
                    throw new Exception("User not found");
                }

                _accountService.Login(identity, HttpContext);
            }
            catch
            {
                ModelState.AddModelError("LoginError", "Аккаунт не найден. Проверьте логин и пароль");
                return View(loginViewModel);
            }
            return RedirectToActionPermanent("Index", "Home");
        }

        [Authorize]
        public IActionResult Logout()
        {
            try
            {
                _accountService.Logout(HttpContext);
            }
            catch (Exception ex)
            {
                Log($"{ex.ToString()}");
            }
            return RedirectToAction("Login", "Account");
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

        private void Log(string v)
        {
            Debug.WriteLine($"\n\n\n{v}\n\n\n");
        }
    }
}
