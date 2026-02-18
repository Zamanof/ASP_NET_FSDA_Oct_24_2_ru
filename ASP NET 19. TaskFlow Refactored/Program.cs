using ASP_NET_19._TaskFlow_Refactored.Extensions;

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