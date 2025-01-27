using Microsoft.EntityFrameworkCore;
using To_do_API.Models.Domain;

namespace To_do_API.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> dbContext)
            :base(dbContext)
        {
            
        }

        public DbSet<Tasks> TasksTable { get; set; }
    }
}
