using DiplomMagister.Classes.Tests;
using DiplomMagister.Data;
using DiplomMagister.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomMagister.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tests = _context.Tests.Include(x => x.Tags).Include(x => x.TestInfo).Where(x => x.Visibility == Classes.Tests.Visibility.Visible).ToList();
            if (tests != null && tests.Count() != 0)
            {
                return View(tests);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost][ValidateAntiForgeryToken]
        public IActionResult Create(CreateTestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var creator = _context.UserClients.FirstOrDefault(x => x.Id.Equals(HttpContext.User.Identity.Name));

            Test test = new Test()
            {
                Creator = creator,
                Title = model.Tittle,
                Description = model.Description,
                TestInfo = new TestInfo()
                {
                    Created = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    Mark = 0
                },
            };

            _context.Tests.Add(test);
            _context.SaveChanges();

            return RedirectToAction("Index", "Test");
        }
    }
}
