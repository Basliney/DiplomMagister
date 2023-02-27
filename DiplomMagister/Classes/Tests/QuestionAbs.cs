using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

    public enum QuestionType
    {
        [Description("1 из 4")]
        [Display(Name = "1 из 4")]
        Basic = 0,
    }
}
