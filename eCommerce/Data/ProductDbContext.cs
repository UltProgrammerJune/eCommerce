using Microsoft.EntityFrameworkCore;
using eCommerce.Models;

namespace eCommerce.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions options) : base(options)
    {
    }

    // Entities to be tracked by DbContext
    public DbSet<Product> Products { get; set; }
}
