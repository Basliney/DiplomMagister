using System.ComponentModel.DataAnnotations;

namespace DiplomMagister.Classes.Tests
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string ShortName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime Created { get; set; }

        public List<Test> Tests { get; set; } = new List<Test>();
    }
}