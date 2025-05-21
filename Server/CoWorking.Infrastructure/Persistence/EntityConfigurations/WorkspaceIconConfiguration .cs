using CoWorking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CoWorking.Infrastructure.Persistence.EntityConfigurations
{
    internal class WorkspaceIconConfiguration : IEntityTypeConfiguration<WorkspaceIcon>
    {
        public void Configure(EntityTypeBuilder<WorkspaceIcon> builder)
        {
            builder.HasKey(wi => new { wi.WorkspaceId, wi.IconId });

            builder.HasOne(wi => wi.Workspace)
                .WithMany(w => w.WorkspaceIcons)
                .HasForeignKey(wi => wi.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wi => wi.Icon)
                .WithMany(i => i.WorkspaceIcons)
                .HasForeignKey(wi => wi.IconId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
