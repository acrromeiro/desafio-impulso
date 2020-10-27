using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace desafio_impulso_dotnet.Models
{
    public class SchoolClass : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Grade { get; set; }
        [Required]
        public int QtdStudents { get; set; }
        [ForeignKey("SchoolId")]
        public School School { get; set; }
        public int SchoolId { get; set; }
    }
}