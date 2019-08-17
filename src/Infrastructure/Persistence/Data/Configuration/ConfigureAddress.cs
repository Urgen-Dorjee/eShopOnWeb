using eWebShop.Application.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eWebShop.Persistence.Data.Configuration
{
    public class ConfigureAddress : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.ZipCode).HasMaxLength(18).IsRequired();
            builder.Property(a => a.Street).HasMaxLength(180).IsRequired();
            builder.Property(a => a.State).HasMaxLength(60);
            builder.Property(a => a.Country).HasMaxLength(90).IsRequired();
            builder.Property(a => a.City).HasMaxLength(100).IsRequired();
        }
    }
}
