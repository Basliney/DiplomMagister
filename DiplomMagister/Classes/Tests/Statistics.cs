using DiplomMagister.Classes.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace DiplomMagister.Classes.Tests
{
    public class Statistics
    {
        public int Id { get; set; }
        public Test Test { get; set; }
        public UserClient UserClient { get; set; }

        public int Persent { get; set; }
        public int Mark { get; set; }
        public DateTime Completed { get; set; } = DateTime.Now;
    }

    public enum Visibility
    {
        [Description("Скрытый")]
        [Display(Name = "Скрытый")]
        Hidden = 0,

        [Description("Доступ по ссылке")]
        [Display(Name = "Доступ по ссылке")]
        OnlyForLink = 1,

        [Description("Видимый")]
        [Display(Name = "Видимый")]
        Visible = 2,
    }
}