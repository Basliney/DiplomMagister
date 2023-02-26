using JWT_Example_ASP.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiplomMagister.Classes.Client
{
    public class UserClient
    {
        [Key]
        public string Id { get; set; }
        //public UserData UserData { get; set; }
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// Избранные тэги
        /// </summary>
        public List<Tag> Favorites { get; set; }
    }

    [Owned]
    public class UserInfo
    {
        public string Name { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string? Middlename { get; set; }
        public string Lastname { get; set; } = "";
        public string Mail { get; set; } = "";

        public Privacy Privacy { get; set; }
    }

    public enum Privacy
    {
        [Description("Скрытый")]
        [Display(Name = "Скрытый")]
        Hidden = 0,

        [Description("Закрытый")]
        [Display(Name = "Закрытый")]
        Private = 1,

        [Description("Открытый")]
        [Display(Name = "Открытый")]
        Public = 2,

        [Description("Пользовательский")]
        [Display(Name = "Пользовательский")]
        Custom = 4,
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
