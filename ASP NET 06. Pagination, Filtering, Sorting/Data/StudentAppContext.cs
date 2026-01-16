using ASP_NET_06._Pagination__Filtering__Sorting.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_06._Pagination__Filtering__Sorting.Data;

public class StudentAppContext : DbContext
{
    public StudentAppContext(DbContextOptions options) 
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; } = default!;
}
