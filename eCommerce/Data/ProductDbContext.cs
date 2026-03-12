using Microsoft.EntityFrameworkCore;
using eCommerce.Models;

namespace eCommerce.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ensures the Member Username is unique 
        modelBuilder.Entity<Member>()
            .HasIndex(m => m.Name)
            .IsUnique();
        // Ensures the Member Email is unique 
        modelBuilder.Entity<Member>()
            .HasIndex(m => m.Email)
            .IsUnique();

    }

    // Entities to be tracked by DbContext
    public DbSet<Product> Products { get; set; }

    public DbSet<Member> Members { get; set; }
}
