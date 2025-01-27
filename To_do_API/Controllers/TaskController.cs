using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using To_do_API.Data;
using To_do_API.Models.Domain;
using To_do_API.Models.DTOs;
using To_do_API.Repository.Interfaces;


namespace To_do_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _context;
        private readonly ITaskRepository _repository;

        public TaskController(TaskDbContext context, ITaskRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]

        public async Task<ActionResult<List<Tasks>>> GetAllTasks()
        {
            // Later on we'll use Repositories to avoide direct communication with the database.

            //Get data from Database - Domain Models
         //  var tasksDomain =  await _context.TasksTable.ToListAsync();

            var tasks = await _repository.GetAllTasksAsync();


            // Map Domain Models to DTOs

            var taskDTO = new List<TaskDTO>();
            foreach (var item in tasks)
            {
                taskDTO.Add(new TaskDTO()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                });
            }

            // Return DTOs to the client .
            return Ok(tasks);

        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Tasks>> GetById(int id)
        {
            // Get Domain model from database.
          var taskDomain = await _context.TasksTable.FindAsync(id);

            if(taskDomain == null)
            {
                return NotFound();
            }

            // Map Task domain model to TaskDTO

            var taskDTO = new TaskDTO
            {
                Id = taskDomain.Id,
                Title = taskDomain.Title,
                Description = taskDomain.Description,
            };

            // Return DTO back to client.
            return Ok(taskDTO);
        }


        [HttpPost]

        public async Task<ActionResult<Tasks>> AddTask([FromBody] PostTaskRequestDTO task)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map or Convert DTO to Domain Model

            var tasksDomain = new Tasks
            {
               
                Title = task.Title,
                Description = task.Description,
            };

            // task.Id = _tasks.Count >= 3 ? _tasks.Max(x => x.Id) + 1 : 4;


            // Use Domain Model to create Tasks.

            _context.TasksTable.Add(tasksDomain);

           await  _context.SaveChangesAsync();


            // _context.TasksTable.Add(task);

            // Map Domain Model back to DTO

            var taskdto = new TaskDTO
            {
                Id = tasksDomain.Id,
                Title = tasksDomain.Title,
                Description = tasksDomain.Description,
            };


            return CreatedAtAction(nameof(GetById), new { id = taskdto.Id }, taskdto);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateTask([FromRoute] int id, [FromBody] PutTaskRequestDTO task)
        {

            var taskDomainModel = await _context.TasksTable.FindAsync(id);

            if(taskDomainModel == null)
            {
                return NotFound(new {message = "We can't find the record so, we can't update it."});
            }

            // Map DTO to Domain Model

            taskDomainModel.Title = task.Title;
            taskDomainModel.Description = task.Description;



           //_context.Entry(task).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            // Convert Domain Model to DTO

            var taskDTO = new TaskDTO
            {
                Id = taskDomainModel.Id,
                Title = taskDomainModel.Title,
                Description = taskDomainModel.Description,
            };


            return Ok(taskDTO);
        }

        // Delete Task.
        

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteTask([FromRoute] int id)
        {

            var taskDomainModel = await _context.TasksTable.FindAsync(id);

           if (taskDomainModel == null)
            {
                return NotFound();
            }


            _context.TasksTable.Remove(taskDomainModel);

           await _context.SaveChangesAsync();

            // return Deleted task back to the client.

            // Map domain model to dto

            var taskDTO = new TaskDTO
            {
                Id = taskDomainModel.Id,
                Title = taskDomainModel.Title,
                Description = taskDomainModel.Description,
            };
            
            
            return Ok(taskDTO);
        }

    }
}
