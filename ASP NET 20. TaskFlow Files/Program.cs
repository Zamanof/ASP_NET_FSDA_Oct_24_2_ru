using ASP_NET_20._TaskFlow_Files.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwagger()
                .AddTaskflowDbContext(builder.Configuration)
                .AddIdentityAndDb(builder.Configuration)
                .AddAuthenticationAndAuthorization(builder.Configuration)
                .AddCorsPolicy()
                .AddFluentValidation()
                .AddAutoMapperAndServices();

var app = builder.Build();

app.UseTaskFlowPipeLine();

await app.EnsureRolesSeededAsync();

app.Run();