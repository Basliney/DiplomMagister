using System.ComponentModel.DataAnnotations;

namespace DiplomMagister.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        [Display(Name = "Введите почту или номер телефона")]
        public string Login { get; set; } = "";

        [Required(ErrorMessage = "Не указан пароль")]
        [Display(Name = "Введите пароль")]
        public string Password { get; set; } = "";
    }
}
