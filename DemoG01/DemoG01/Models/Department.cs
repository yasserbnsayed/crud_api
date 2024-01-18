using System.ComponentModel.DataAnnotations;

namespace DemoG01.Models
{
    public class Department
    {
        public int Id { get; set; }
        [MinLength(2)]
        [MaxLength(25, ErrorMessage ="Length must be less than 6 Char.")]  
        public string Name { get; set; }    
        public string MangerName { get; set; }
    }
}
