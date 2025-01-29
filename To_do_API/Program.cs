using Microsoft.EntityFrameworkCore;
using To_do_API.Data;
using To_do_API.Repository.Interfaces;
using To_do_API.Repository.Concret_Classes;
using To_do_API.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TasksDb")));

// Register Dependencies...

builder.Services.AddScoped<ITaskRepository, SQLTaskRepository>();   


// Register Automapper

builder.Services.AddAutoMapper(typeof(AutomapperProfiles));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
