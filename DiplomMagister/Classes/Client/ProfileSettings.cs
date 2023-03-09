using DiplomMagister.Classes.Client;
using System.ComponentModel.DataAnnotations;

namespace JWT_Example_ASP.Models
{
    public class ProfileSettings
    {
        [Key]
        public int Id { get; set; }

        public string? Login { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; }
        public DateTime? CreationDate { get; set; }

        public UserClient Owner { get; set; }
    }
}
