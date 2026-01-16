using ASP_NET_06._Pagination__Filtering__Sorting.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StudentAppContext>(
    op => op.UseSqlServer(
        builder.Configuration.GetConnectionString("StudentsAppCS"),
        s =>
        {
            s.CommandTimeout(30);
            s.MigrationsHistoryTable("EF_TABLE_MIGRATONS");
        }
        )
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
