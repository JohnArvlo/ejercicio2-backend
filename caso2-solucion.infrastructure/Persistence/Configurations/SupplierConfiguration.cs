using caso2_solucion.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace caso2_solucion.infrastructure.Persistence.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.LegalName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.TradeName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.TaxId)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Website)
                .HasMaxLength(300);

            builder.Property(p => p.PhysicalAddress)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(p => p.Country)
                .IsRequired();

            builder.Property(p => p.AnnualRevenueUsd)
                .HasPrecision(18, 2);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.LastModifiedAt)
                .IsRequired();

            builder.Property(p => p.IsDeleted)
                .IsRequired();
        }
    }
}
