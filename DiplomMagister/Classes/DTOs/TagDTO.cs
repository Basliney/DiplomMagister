using DiplomMagister.Classes.Client;
using DiplomMagister.Classes.Tests;

namespace DiplomMagister.Classes.DTOs
{
    public class TagDTO
    {
        public int Id { get; set; }
        public Tag Tag { get; set; }
        public UserClient UserClient { get; set; }
    }
}
