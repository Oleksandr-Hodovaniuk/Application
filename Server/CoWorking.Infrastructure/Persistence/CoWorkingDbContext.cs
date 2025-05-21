using CoWorking.Core.Entities;
using CoWorking.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CoWorking.Infrastructure.Persistence;

internal class CoWorkingDbContext(DbContextOptions<CoWorkingDbContext> options) 
    : DbContext(options)
{
    internal DbSet<Workspace> Workspaces { get; set; }
    internal DbSet<Space> SpaceConfigurations { get; set; }
    internal DbSet<Booking> Bookings { get; set; }
    internal DbSet<Picture> Pictures { get; set; }
    internal DbSet<Icon> Icons { get; set; }
    internal DbSet<WorkspaceIcon> WorkspaceIcons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Entity configurations.
        modelBuilder.ApplyConfiguration(new WorkspaceIconConfiguration());
        modelBuilder.ApplyConfiguration(new SpaceConfiguration());
    }
}
