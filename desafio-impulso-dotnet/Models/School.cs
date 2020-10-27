using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace desafio_impulso_dotnet.Models
{
    public class School : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public List<SchoolClass> SchoolClasses { get; set; }
    }
}