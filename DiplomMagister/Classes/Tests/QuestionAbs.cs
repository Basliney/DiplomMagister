using DiplomMagister.Classes.Exceptions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DiplomMagister.Classes.Tests
{
    public abstract class QuestionAbs
    {
        [Key]
        public abstract int Id { get; set; }
        [Required]
        public abstract string Title { get; set; }
        [Required]
        public abstract string Essence { get; set; }
        [Required]
        public abstract int TotalScore { get; set; }
        [Required]
        public abstract QuestionType QuestionType { get; set; }
        [Required]
        public abstract DateTime Created { get; set; }
    }

    public static class QuestionJSONParser
    {
        public static QuestionAbs FromJSON(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new System.NullReferenceException("Text is null or empty");
            }

            QuestionAbs result;
            string parsalebleText = text.Replace(" ", ""); 
            Regex regex = new Regex(@"questiontype:(\d*)");
            if (int.TryParse(regex.Matches(parsalebleText.ToLower()).FirstOrDefault()?.Value, out int type))
            {
                switch (type)
                {
                    case ((int)QuestionType.Basic):
                        result = JsonSerializer.Deserialize<BasicQuestion>(text, options: new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                        break;
                        
                    case ((int)QuestionType.Short):
                        result = JsonSerializer.Deserialize<ShortQuestion>(text, options: new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                        break;

                    default:
                        throw new JsonException($"Can't parse from JSON: {text}");
                }
            }
            else
            {
                throw new RegexException($"Regex can't find type: {text}");
            }
            return result;
        }
        
        public static string ToJSON(QuestionAbs questionAbs)
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
