using DiplomMagister.Classes.Client;

namespace DiplomMagister.Classes
{
    public class TagDTO
    {
        public int Id { get; set; }
        public Tag Tag { get; set; }
        public UserClient UserClient { get; set; }
    }
}
