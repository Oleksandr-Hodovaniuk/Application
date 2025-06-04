using CoWorking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoWorking.Infrastructure.Persistence.EntityConfigurations;

internal class CoworkingConfiguration : IEntityTypeConfiguration<Coworking>
{
    public void Configure(EntityTypeBuilder<Coworking> builder)
    {
        builder.HasKey(c => c.Id);

        builder.OwnsOne(c => c.Addresses);

        builder.HasMany(c => c.Workspaces)
            .WithOne(w => w.Coworking)
            .HasForeignKey(w => w.CoworkingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
