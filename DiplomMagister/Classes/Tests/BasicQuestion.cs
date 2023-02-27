namespace DiplomMagister.Classes.Tests
{
    public class BasicQuestion : QuestionAbs
    {
        #region Базовый класс
        public override int Id { get; set; }
        public override string Title { get; set; }
        public override string Essence { get; set; }
        public override int TotalScore { get; set; }
        public override QuestionType QuestionType { get; set; } = QuestionType.Basic;
        public override DateTime Created { get; set; } = DateTime.Now;
        #endregion

        public List<string> Answers { get; set; } = new List<string>();
        public int IndexOfTrue { get; set; } = -1;
    }
}
