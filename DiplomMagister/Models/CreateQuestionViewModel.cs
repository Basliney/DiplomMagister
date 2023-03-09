using DiplomMagister.Classes.Tests;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DiplomMagister.Models
{
    public class CreateQuestionViewModel
    {
        [Required(ErrorMessage = "Не заголовок вопроса")]
        [Display(Name = "Введите заголовок вопроса")]
        public string Tittle { get; set; } = "";

        [Required(ErrorMessage = "Не указана суть вопроса")]
        [Display(Name = "Введите суть вопроса")]
        public string Essence { get; set; }

        [Required(ErrorMessage = "Не указано влияние вопроса на результат")]
        [Display(Name = "Укажите влияние вопроса")]
        public int TotalScore { get; set; }

        [Required(ErrorMessage = "Не выбран тип вопроса")]
        [Display(Name = "Выберите тип вопроса")]
        public QuestionType QuestionType { get; set; }

        public int TestId { get; set; }

        #region Short и Basic вопросы
        /// <summary>
        /// Выборочно!
        /// Только при базовом и укороченом вопросе
        /// </summary>
        public string[] Answers { get; set; }
        public int IndexOfTrue { get; set; }
        #endregion
    }
}