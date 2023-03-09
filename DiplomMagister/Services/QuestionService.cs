using DiplomMagister.Classes.Tests;
using DiplomMagister.Data;
using DiplomMagister.Models;

namespace DiplomMagister.Services
{
    internal class QuestionService
    {
        private readonly ApplicationDbContext _context;

        public QuestionService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IQuestionAbs Create(CreateQuestionViewModel model)
        {
            IQuestionAbs question;
            switch (model.QuestionType)
            {
                case QuestionType.Basic:
                    ParseBasicQuestion(model, out question);
                    break;

                default: throw new Exception("Invalid type of question");
            }

            return question;
        }

        private void ParseBasicQuestion(CreateQuestionViewModel model, out IQuestionAbs question)
        {
            question = new BasicQuestion()
            {
                Created = DateTime.UtcNow,
                Essence = model.Essence,
                QuestionType = QuestionType.Basic,
                Title = model.Tittle,
                TotalScore = model.TotalScore,
            };
        }
    }
}