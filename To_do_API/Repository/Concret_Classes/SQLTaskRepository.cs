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

        public async Task<List<Tasks>> GetAllTasksAsync()
        {
          return await _context.TasksTable.ToListAsync();
        }
    }
}
