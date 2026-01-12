using ASP_NET_04._Products_MVC_Scaffold.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_04._Products_MVC_Scaffold.Data;

public class ProductsMVCContext : DbContext
{
    public ProductsMVCContext(DbContextOptions options) 
        : base(options)
    {}

    public DbSet<Product> Products => Set<Product>();
}
