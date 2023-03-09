using DiplomMagister.Classes.Exceptions;
using DiplomMagister.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DiplomMagister.Classes.Tests
{
    public interface IQuestionAbs
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Essence { get; set; }
        public int TotalScore { get; set; }
        public QuestionType QuestionType { get; set; }
        public DateTime Created { get; set; }

        public IQuestionAbs ParseFromJSON(string text);

        public CreateQuestionViewModel ParseFromJSONToViewModel(string text, int testId);
    }

    public static class QuestionJSONParser
    {
        public static IQuestionAbs FromJSON(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new System.NullReferenceException("Text is null or empty");
            }

            string parsalebleText = text.Replace(" ", "");
            Regex regex = new Regex(@"""questiontype"":(\d*)");
            var RegexResult = regex.Matches(parsalebleText.ToLower());
            if (int.TryParse(RegexResult.FirstOrDefault()?.Value.Replace(@"""questiontype"":", ""), out int type))
            {
                switch (type)
                {
                    case ((int)QuestionType.Basic):
                        return new BasicQuestion().ParseFromJSON(text);

                    case ((int)QuestionType.Short):
                        return new ShortQuestion().ParseFromJSON(text);

                    default:
                        throw new JsonException($"Can't parse from JSON: {text}");
                }
            }
            else
            {
                throw new RegexException($"Regex can't find type: {text}");
            }
        }

        public static CreateQuestionViewModel FromJSONToCreateQuestionViewModel(string text, int testId = -1)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new System.NullReferenceException("Text is null or empty");
            }

            string parsalebleText = text.Replace(" ", "");
            Regex regex = new Regex(@"""questiontype"":(\d*)");
            var RegexResult = regex.Matches(parsalebleText.ToLower());
            if (int.TryParse(RegexResult.FirstOrDefault()?.Value.Replace(@"""questiontype"":", ""), out int type))
            {
                switch (type)
                {
                    case ((int)QuestionType.Basic):
                        return new BasicQuestion().ParseFromJSONToViewModel(text, testId);

                    case ((int)QuestionType.Short):
                        return new ShortQuestion().ParseFromJSONToViewModel(text, testId);

                    default:
                        throw new JsonException($"Can't parse from JSON: {text}");
                }
            }
            else
            {
                throw new RegexException($"Regex can't find type: {text}");
            }
        }
        
        public static string ToJSON(IQuestionAbs questionAbs)
        {
            return JsonSerializer.Serialize(questionAbs, options: new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }

    public enum QuestionType
    {
        [Description("1 из 4")]
        [Display(Name = "1 из 4")]
        Basic = 0,

        [Description("1 из 2")]
        [Display(Name = "1 из 2")]
        Short = 1,
    }
}
