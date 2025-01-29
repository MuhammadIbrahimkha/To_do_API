using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Collections.Generic;
using To_do_API.CustomActionFilter;
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
        private readonly IMapper _mapper;

        public TaskController(TaskDbContext context, ITaskRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<ActionResult<List<Tasks>>> GetAllTasks()
        {
            // Later on we'll use Repositories to avoide direct communication with the database.

            //Get data from Database - Domain Models
         //  var tasksDomain =  await _context.TasksTable.ToListAsync();

            var tasks = await _repository.GetAllTasksAsync();


            // Map Domain Models to DTOs

            //var taskDTO = new List<TaskDTO>();
            //foreach (var item in tasks)
            //{
            //    taskDTO.Add(new TaskDTO()
            //    {
            //        Id = item.Id,
            //        Title = item.Title,
            //        Description = item.Description,
            //    });
            //}


            //Map Domain Models to DTOs

          var taskDto =  _mapper.Map<List<TaskDTO>>(tasks);



            // Return DTOs to the client .
            return Ok(taskDto);

        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Tasks>> GetById(int id)
        {
            // Get Domain model from database.
       //   var taskDomain = await _context.TasksTable.FindAsync(id);

            var taskDomain = await _repository.GetByIdAsync(id);

            if(taskDomain == null)
            {
                return NotFound();
            }

            // Map Task domain model to TaskDTO

            //var taskDTO = new TaskDTO
            //{
            //    Id = taskDomain.Id,
            //    Title = taskDomain.Title,
            //    Description = taskDomain.Description,
            //};

            var taskDTO = _mapper.Map<TaskDTO>(taskDomain);

            // Return DTO back to client.
            return Ok(taskDTO);
        }


        [HttpPost]
        [ValidateModel]

        public async Task<ActionResult<Tasks>> AddTask([FromBody] PostTaskRequestDTO task)
        {

            // instead of this we're using the above attribute which is : 'ValidateModel'.

            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            // Map or Convert DTO to Domain Model

            //var tasksDomain = new Tasks
            //{

            //    Title = task.Title,
            //    Description = task.Description,
            //};

            var tasksDomain = _mapper.Map<Tasks>(task);

            // task.Id = _tasks.Count >= 3 ? _tasks.Max(x => x.Id) + 1 : 4;


            // Use Domain Model to create Tasks.

            //_context.TasksTable.Add(tasksDomain);

          tasksDomain =  await _repository.AddTaskAsync(tasksDomain);



            // _context.TasksTable.Add(task);

            // Map Domain Model back to DTO

            //var taskdto = new TaskDTO
            //{
            //    Id = tasksDomain.Id,
            //    Title = tasksDomain.Title,
            //    Description = tasksDomain.Description,
            //};

            // Map Domain Model back to DTO

            var taskdto = _mapper.Map<TaskDTO>(tasksDomain);


            return CreatedAtAction(nameof(GetById), new { id = taskdto.Id }, taskdto);
        }


        [HttpPut("{id}")]
        [ValidateModel]

        public async Task<ActionResult> UpdateTask([FromRoute] int id, [FromBody] PutTaskRequestDTO task)
        {

            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}


            // Map DTO to domain model.

            //var tastsDomainModel = new Tasks
            //{
            //    Title = task.Title,
            //    Description = task.Description,
            //};

            var tastsDomainModel = _mapper.Map<Tasks>(task);



            //  var taskDomainModel = await _context.TasksTable.FindAsync(id);

          tastsDomainModel =  await _repository.UpdateTaskAsync(id, tastsDomainModel);

            if(tastsDomainModel == null)
            {
                return NotFound(new {message = "We can't find the record so, we can't update it."});
            }

           

           //_context.Entry(task).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            // Convert Domain Model to DTO

            //var taskDTO = new TaskDTO
            //{
            //    Id = tastsDomainModel.Id,
            //    Title = tastsDomainModel.Title,
            //    Description = tastsDomainModel.Description,
            //};


            var taskDTO = _mapper.Map<TaskDTO>(tastsDomainModel);

            return Ok(taskDTO);
        }

        // Delete Task.
        

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteTask([FromRoute] int id)
        {

            // var taskDomainModel = await _context.TasksTable.FindAsync(id);

            var taskDomainModel = await _repository.DeleteTaskAsync(id);



           if (taskDomainModel == null)
            {
                return NotFound();
            }

            // return Deleted task back to the client.

            // Map domain model to dto

            var taskDTO = new TaskDTO
            {
                Id = taskDomainModel.Id,
                Title = taskDomainModel.Title,
                Description= taskDomainModel.Description,

            };
            
            return Ok(taskDTO);
        }

    }
}
