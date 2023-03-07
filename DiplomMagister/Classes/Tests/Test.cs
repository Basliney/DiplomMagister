using DiplomMagister.Classes.Client;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiplomMagister.Classes.Tests
{
    public class Test
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public UserClient Creator { get; set; }
        [Required]
        public TestInfo TestInfo { get; set; } = new TestInfo();

        /// <summary>
        /// JSON serialized QuestionAbs objects
        /// </summary>
        public List<string>? Questions { get; set; } = new List<string>();
 
        public List<Tag>? Tags { get; set; } = new List<Tag>();
        public List<Statistics>? Statistics { get; set; } = new List<Statistics>();
        public Visibility Visibility { get; set; } = Visibility.Visible;
    }
}
