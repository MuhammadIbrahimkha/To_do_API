using To_do_API.Models.Domain;

namespace To_do_API.Repository.Interfaces
{
    public interface ITaskRepository
    {
       Task<List<Tasks>> GetAllTasksAsync();
    }
}
