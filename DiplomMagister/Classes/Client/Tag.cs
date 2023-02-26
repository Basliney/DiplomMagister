using System.ComponentModel.DataAnnotations;

namespace DiplomMagister.Classes.Client
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string ShortName { get; set; } = String.Empty;
        public string FullName { get; set; } = String.Empty;
        public DateTime Created {get;set;}
    }
}