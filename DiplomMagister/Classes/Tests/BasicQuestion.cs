using DiplomMagister.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace DiplomMagister.Classes.Tests
{
    public class BasicQuestion : IQuestionAbs
    {
        #region Базовый класс
        public int Id { get; set; }
        public string Title { get; set; }
        public string Essence { get; set; }
        public int TotalScore { get; set; }
        public QuestionType QuestionType { get; set; } = QuestionType.Basic;
        public DateTime Created { get; set; } = DateTime.Now;
        #endregion

        public string[] Answers { get; set; } = new string[4];
        public int IndexOfTrue { get; set; } = -1;

        public IQuestionAbs ParseFromJSON(string text)
        {
            BasicQuestion parsingResult = JsonSerializer.Deserialize<BasicQuestion>(text, options: new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            if (parsingResult == null) { return null; }
            return new BasicQuestion()
            {
                QuestionType = parsingResult.QuestionType,
                Answers = parsingResult.Answers,
                Created = parsingResult.Created,
                Essence = parsingResult.Essence ,
                Id = parsingResult.Id,
                IndexOfTrue = parsingResult.IndexOfTrue,
                Title = parsingResult.Title,
                TotalScore = parsingResult.TotalScore 
            };
        }

        public CreateQuestionViewModel ParseFromJSONToViewModel(string text, int testId)
        {
            BasicQuestion question = JsonSerializer.Deserialize<BasicQuestion>(text, options: new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
