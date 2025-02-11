using Microsoft.EntityFrameworkCore;

namespace CreationHub.Models;

public class NicePartUsageContext : DbContext
{
    public NicePartUsageContext(DbContextOptions<NicePartUsageContext> options)
        : base(options)
    {
    }

    public DbSet<NicePartUsage> NicePartUsages { get; set; } = null!;
}