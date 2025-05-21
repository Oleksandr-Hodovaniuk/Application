using CoWorking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoWorking.Infrastructure.Persistence.EntityConfigurations;

internal class PictureConfiguration : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        builder.HasKey(p => p.Id);

        // Picture + Workspace configuration.
        builder.HasOne(p => p.Workspace)
            .WithMany(w => w.Pictures)
            .HasForeignKey(p => p.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
