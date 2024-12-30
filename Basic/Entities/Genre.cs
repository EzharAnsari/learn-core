using System.ComponentModel.DataAnnotations;
using Basic.Validations;

namespace Basic.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage ="This field with name {0} is required")]
        [StringLength(50)]
        [FirstLetterUppercase] 
        public string Name { get; set; }
    }
}