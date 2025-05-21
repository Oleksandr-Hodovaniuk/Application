using CoWorking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoWorking.Infrastructure.Persistence.EntityConfigurations;

internal class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(s => s.Id);

        // Space + Workspace configuration.
        builder.HasOne(s => s.Workspace)
            .WithMany(w => w.RoomConfigurations)
            .HasForeignKey(s => s.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
