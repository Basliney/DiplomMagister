using DiplomMagister.Classes;
using DiplomMagister.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DiplomMagister.Controllers
{
    public class TokenController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public TokenController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
