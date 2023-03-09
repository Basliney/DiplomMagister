using DiplomMagister.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace DiplomMagister.Classes.Tests
{
    public class ShortQuestion : IQuestionAbs
    {
        #region Базовый класс
        public int Id { get; set; }
        public string Title { get; set; }
        public string Essence { get; set; }
        public int TotalScore { get; set; }
        public QuestionType QuestionType { get; set; } = QuestionType.Short;
        public DateTime Created { get; set; } = DateTime.Now;
        #endregion

        public string[] Answers { get; set; } = new string[2];
        public int IndexOfTrue { get; set; } = -1;

        public IQuestionAbs ParseFromJSON(string text)
        {
            ShortQuestion parsingResult = JsonSerializer.Deserialize<ShortQuestion>(text, options: new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            if (parsingResult == null) { return null; }
            return new BasicQuestion()
            {
                QuestionType = parsingResult.QuestionType,
                Answers = parsingResult.Answers,
                Created = parsingResult.Created,
                Essence = parsingResult.Essence,
                Id = parsingResult.Id,
                IndexOfTrue = parsingResult.IndexOfTrue,
                Title = parsingResult.Title,
                TotalScore = parsingResult.TotalScore
            };
        }

        public CreateQuestionViewModel ParseFromJSONToViewModel(string text, int testId)
        {
            ShortQuestion question = JsonSerializer.Deserialize<ShortQuestion>(text, options: new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            CreateQuestionViewModel createQuestionViewModel = new CreateQuestionViewModel()
            {
                Answers = question.Answers,
                Essence = question.Essence,
                Tittle = question.Title,
                QuestionType = question.QuestionType,
                IndexOfTrue = question.IndexOfTrue,
                TestId = testId,
                TotalScore = question.TotalScore,
            };
            return createQuestionViewModel;
        }
    }
}
