using System.ComponentModel.DataAnnotations;

namespace To_do_API.Models.DTOs
{
    public class TaskDTO
    {
        // This DTO is a kind of subset of the Task domain model.
        // So, now the client will be 
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
