using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using To_do_API.Data;
using To_do_API.Models.Domain;
using To_do_API.Repository.Interfaces;

namespace To_do_API.Repository.Concret_Classes
{

    public class SQLTaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public SQLTaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        // For Post Method in the controller.
        public async Task<Tasks> AddTaskAsync(Tasks tasks)
        {
          await  _context.TasksTable.AddAsync(tasks);

            await _context.SaveChangesAsync();

            return tasks;
        }

        public async Task<Tasks> DeleteTaskAsync(int id)
        {
            var existingTask = await _context.TasksTable.FirstOrDefaultAsync(t => t.Id == id);

            if (existingTask == null)
            {
                return null;
            }

            _context.TasksTable.Remove(existingTask);

            await _context.SaveChangesAsync();

            return existingTask;

        }

        // For Get Method in the controller.
        public async Task<List<Tasks>> GetAllTasksAsync()
        {
          return await _context.TasksTable.ToListAsync();
        }

        // For GetById Method in the controller.

        public async Task<Tasks?> GetByIdAsync(int id)
        {
           return await _context.TasksTable.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tasks> UpdateTaskAsync(int id, Tasks tasks)
        {
            var existingtask = await _context.TasksTable.FirstOrDefaultAsync(x =>x.Id == id);

            if(existingtask == null)
            {
                return null;
            }

            existingtask.Title = tasks.Title;
            existingtask.Description = tasks.Description;

            await _context.SaveChangesAsync();

            return existingtask;    
        }
    }
}
