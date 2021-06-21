using Clinic.Core.Entities;
using Clinic.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class ConsultingRoomConfigurations : IEntityTypeConfiguration<ConsultingRoom>
    {
        public void Configure(EntityTypeBuilder<ConsultingRoom> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NameIdentifier)
                .HasMaxLength(10)
                .IsRequired();

            builder.HasIndex(c => c.NameIdentifier)
                .IsUnique();

            builder.Property(c => c.EntityStatus)
                .IsRequired()
                .HasConversion(x => x.ToString(),
                               x => (EntityStatus)Enum.Parse(typeof(EntityStatus), x));

            builder.HasMany<Medic>(c => c.Medics)
                .WithOne(m => m.ConsultingRoom)
                .HasForeignKey(m => m.IdConsultingRoom)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
