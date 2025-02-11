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
    
}