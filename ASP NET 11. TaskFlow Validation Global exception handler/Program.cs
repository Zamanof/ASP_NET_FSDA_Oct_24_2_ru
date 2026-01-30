using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Data;
using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.DTOs;
using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.DTOs.TaskItem_DTOs;
using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Mappings;
using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Middlewares;
using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Services;
using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Services.Interfaces;
using ASP_NET_11._TaskFlow_Validation_Global_exception_handler.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "TaskFlow API",
            Description = "API for project and task management. This API provides a full set of CRUD operations for working with projects and tasks.",
            Contact = new OpenApiContact
            {
                Name = "TaskFlow Team",
                Email = "support@taskflow.com"
            },
            License = new OpenApiLicense
            {
                Name = "MIT Licence",
                Url = new Uri("https://opensource.org/license/mit")
            }
        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);

        //options.SchemaFilter<SwaggerExampleSchemaFilter>();
    }
    );

var connectionString = builder
    .Configuration
    .GetConnectionString("TaskFlowDBConnetionString");

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();

//builder.Services.AddScoped<IValidator<CreateProjectDto>, CreateProjectValidator>();
//builder.Services.AddScoped<IValidator<UpdateProjectDto>, UpdateProjectValidator>();
//builder.Services.AddScoped<IValidator<CreateTaskItemDto>, CreateTaskItemValidtor>();
//builder.Services.AddScoped<IValidator<UpdateTaskItemDto>, UpdateTaskItemValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddFluentValidationAutoValidation();

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
    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskFlow API v1");
            options.RoutePrefix = string.Empty;
            options.DisplayRequestDuration();
            options.EnableDeepLinking();
            options.EnableFilter();
            options.EnableTryItOutByDefault();
        }
        );
    app.MapOpenApi();
}
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
