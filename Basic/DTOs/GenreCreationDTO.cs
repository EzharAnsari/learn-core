using System.ComponentModel.DataAnnotations;
using Basic.Validations;

namespace Basic.DTOs
{
    public class GenreCreationDTO
    {
        [Required(ErrorMessage ="This field with name {0} is required")]
        [StringLength(50)]
        [FirstLetterUppercase] 
        public string Name { get; set; }
    }
}