using ASP_NET_09._TaskFlow_AutoMapper.Data;
using ASP_NET_09._TaskFlow_AutoMapper.Mappings;
using ASP_NET_09._TaskFlow_AutoMapper.Services;
using ASP_NET_09._TaskFlow_AutoMapper.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var connectionString = builder
    .Configuration
    .GetConnectionString("TaskFlowDBConnetionString");

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();

builder.Services.AddDbContext<TaskFlowDbContext>(
    options =>
        options.UseSqlServer(connectionString)
    );

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
