using DiplomMagister.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomMagister.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult GetPhoto()
        {
            try
            {
                var user = _context.UserClients.FirstOrDefault(x => x.Id.Equals(HttpContext.User.Identity.Name));
                if (user == null) { throw new Exception("Unknown user"); }
                if (string.IsNullOrEmpty(user.ProfileInformation.Image)) { throw new Exception("Нет изображения"); }
                return Ok(user.ProfileInformation.Image);
            }
            catch
            {
                return StatusCode(204);
            }
        }

        public IActionResult GetUserClient()
        {
            try
            {
                var user = _context.UserClients.FirstOrDefault(x => x.Id.Equals(HttpContext.User.Identity.Name));
                if (user != null)
                {
                    return Ok(user);
                }
                return StatusCode(204);
            }
            catch
            {
                return StatusCode(204);
            }
        }
    }
}
