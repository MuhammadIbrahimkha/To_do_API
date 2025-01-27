using System.ComponentModel.DataAnnotations;

namespace To_do_API.Models.Domain
{
    public class Tasks
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }


        [StringLength(200)]
        public string Description { get; set; }

    }
}
