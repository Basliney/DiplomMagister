using System.ComponentModel.DataAnnotations;

namespace DiplomMagister.Classes.Tests
{
    public class ShortQuestion : QuestionAbs
    {
        #region Базовый класс
        public override int Id { get; set; }
        public override string Title { get; set; }
        public override string Essence { get; set; }
        public override int TotalScore { get; set; }
        public override QuestionType QuestionType { get; set; } = QuestionType.Short;
        public override DateTime Created { get; set; } = DateTime.Now;
        #endregion

        public string[] Answers { get; set; } = new string[2];
        public int IndexOfTrue { get; set; } = -1;
    }
}
