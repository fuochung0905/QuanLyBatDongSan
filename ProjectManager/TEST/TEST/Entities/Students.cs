using System.ComponentModel.DataAnnotations;

namespace TEST.Models
{
    public class Students
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
