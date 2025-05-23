using CoWorking.Core.Entities;
using CoWorking.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CoWorking.Infrastructure.Persistence;

internal class CoWorkingDbContext(DbContextOptions<CoWorkingDbContext> options) 
    : DbContext(options)
{
    internal DbSet<Icon> Icons { get; set; }
    internal DbSet<Workspace> Workspaces { get; set; }
    internal DbSet<WorkspaceIcon> WorkspaceIcons { get; set; }
    internal DbSet<Room> Rooms { get; set; }
    internal DbSet<Booking> Bookings { get; set; }
    internal DbSet<Picture> Pictures { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Entity configurations.
        modelBuilder.ApplyConfiguration(new WorkspaceConfiguration());
        modelBuilder.ApplyConfiguration(new WorkspaceIconConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new PictureConfiguration());
        modelBuilder.ApplyConfiguration(new BookingConfiguration());
    }
}
