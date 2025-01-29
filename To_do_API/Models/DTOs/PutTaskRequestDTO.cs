using System.ComponentModel.DataAnnotations;

namespace To_do_API.Models.DTOs
{
    public class PutTaskRequestDTO
    {
        [Required]
        [MinLength(10)]
        [MaxLength(20)]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
