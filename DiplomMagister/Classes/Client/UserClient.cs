using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiplomMagister.Classes.Client
{
    public class UserClient
    {
        [Key]
        public string Id { get; set; }
        public List<Token> Token { get; set; } = new List<Token>();

        private Role _role = Role.User;
        public Role Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public UserInfo Userinfo { get; set; }
    }

    [Owned]
    public class UserInfo
    {
        public string Name { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string? Middlename { get; set; }
        public string Lastname { get; set; } = "";
    }

    public enum Role
    {
        [Description("Пользователь")]
        [Display(Name = "Пользователь")]
        User = 0,

        [Description("Продвинутый пользователь")]
        [Display(Name = "Продвинутый пользователь")]
        Creator = 1,

        [Description("Модератор")]
        [Display(Name = "Модератор")]
        Moderator = 2,

        [Description("Администратор")]
        [Display(Name = "Администратор")]
        Admin = 4
    }
}
