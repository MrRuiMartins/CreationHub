using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace CreationHub.Models.NicePartUsage;

public class CreationHubContext : DbContext
{
    public CreationHubContext(DbContextOptions<CreationHubContext> options)
        : base(options)
    {
    }

    public DbSet<NicePartUsage> NicePartUsages { get; set; } = null!;
    
    public DbSet<Rating> Ratings { get; set; } = null!;
    
    public DbSet<User> Users { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Email = "email@example.com", Password = "123456", Role="Admin" }
        );
    }
    
}