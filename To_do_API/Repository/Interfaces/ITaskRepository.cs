using To_do_API.Models.Domain;

namespace To_do_API.Repository.Interfaces
{
    public interface ITaskRepository
    {
       Task<List<Tasks>> GetAllTasksAsync();

        Task<Tasks> GetByIdAsync(int id);

      Task<Tasks> AddTaskAsync(Tasks tasks);

       Task<Tasks> UpdateTaskAsync(int id, Tasks tasks);

      Task<Tasks> DeleteTaskAsync(int id);
    }
}
