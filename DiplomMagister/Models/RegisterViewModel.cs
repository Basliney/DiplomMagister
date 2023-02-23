using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DiplomMagister.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [Display(Name = "Введите фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указана почта")]
        [Display(Name = "Введите почту")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [Display(Name = "Введите пароль")]
        public string Password { get; set; }
    }
}
