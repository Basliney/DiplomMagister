using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DiplomMagister.Models
{
    public class CreateTestViewModel
    {
        [Required(ErrorMessage = "Не указано название теста")]
        [Display(Name = "Введите навание")]
        public string Tittle { get; set; } = "";

        [Required(ErrorMessage = "Не указано описание теста")]
        [Display(Name = "Введите описнаие")]
        public string Description { get; set; } = "";
    }
}
