using System.ComponentModel.DataAnnotations;

namespace To_do_API.Models.Domain
{
    public class Tasks
    {
        public int Id { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
