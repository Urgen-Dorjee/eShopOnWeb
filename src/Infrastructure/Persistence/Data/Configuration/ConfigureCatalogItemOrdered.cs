using eWebShop.Application.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eWebShop.Persistence.Data.Configuration
{
    public class ConfigureCatalogItemOrdered : IEntityTypeConfiguration<CatalogItemOrdered>
    {
        public void Configure(EntityTypeBuilder<CatalogItemOrdered> builder)
        {
            builder.Property(cio => cio.ProductName).HasMaxLength(50).IsRequired();
        }
    }
}
