using CoWorking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoWorking.Infrastructure.Persistence.EntityConfigurations;

internal class SpaceConfiguration : IEntityTypeConfiguration<Space>
{
    public void Configure(EntityTypeBuilder<Space> builder)
    {
        builder.HasKey(s => s.Id);

        // Space + Workspace configuration.
        builder.HasOne(s => s.Workspace)
            .WithMany(w => w.SpaceConfigurations)
            .HasForeignKey(s => s.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
