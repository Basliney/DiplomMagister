using DiplomMagister.Classes.Exceptions;
using DiplomMagister.Classes.Tests;
using DiplomMagister.Data;
using DiplomMagister.Models;
using DiplomMagister.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DiplomMagister.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly QuestionService QuestionService;

        public QuestionController(ApplicationDbContext context)
        {
            _context = context;
            QuestionService = new QuestionService(context);
        }

        // GET: QuestionController
        public ActionResult Index(int testId)
        {
            var questions = _context.Tests.FirstOrDefault(x => x.Id == testId)?.Questions;
            return View(questions);
        }

        // GET: QuestionController/Details/5
        public ActionResult Details(int testId, int index)
        {
            QuestionAbs detailedQuestion;
            try
            {
                detailedQuestion = QuestionJSONParser.FromJSON(_context.Tests.FirstOrDefault(x => x.Id == testId)?.Questions[index]);
                if (detailedQuestion == null)
                {
                    return RedirectToAction(nameof(Index), new { testId = testId });
                }
            }
            catch (RegexException ex)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (JsonException ex)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (NullReferenceException ex)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(detailedQuestion);
        }

        // GET: QuestionController/Create
        public ActionResult Create(int id)
        {
            var test = _context.Tests.FirstOrDefault(x => x.Id == id);
            if (test == null) { return RedirectToAction(nameof(Index), new { testId = id }); }
            ViewBag.TestId = id;
            return View();
        }

        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateQuestionViewModel model)
        {
            QuestionAbs question;
            switch (model.QuestionType)
            {
                case QuestionType.Basic:
                    question = new BasicQuestion()
                    {
                        QuestionType = model.QuestionType,
                        Created = DateTime.UtcNow,
                        Essence = model.Essence,
                        Title = model.Tittle,
                        TotalScore = model.TotalScore,
                        Answers = model.Answers,
                        IndexOfTrue = model.IndexOfTrue
                    };
                    break;

                case QuestionType.Short:
                    question = new ShortQuestion()
                    {
                        QuestionType = model.QuestionType,
                        Created = DateTime.UtcNow,
                        Essence = model.Essence,
                        Title = model.Tittle,
                        TotalScore = model.TotalScore,
                        Answers = model.Answers,
                        IndexOfTrue = model.IndexOfTrue
                    };
                    break;

                default:
                    ModelState.AddModelError("Tittle", "Тип вопроса не определён");
                    return View(model);
            }

            var test = _context.Tests.FirstOrDefault(x => x.Id == model.TestId);
            if (test == null)
            {
                return BadRequest("test wasn't found");
            }
            if (test.Questions == null) { test.Questions = new List<string>(); }
            test.Questions.Add(QuestionJSONParser.ToJSON(question));
            _context.Update(test);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { testId = model.TestId });
        }

        // GET: QuestionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuestionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuestionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
