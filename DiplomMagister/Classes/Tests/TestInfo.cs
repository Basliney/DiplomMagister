using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace DiplomMagister.Classes.Tests
{
    public class TestInfo
    {
        public int Id { get; set; }
        public double Mark { get; set; } = 0;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        //[Required]
        //public Test ParentTest { get; set; }
    }
}
