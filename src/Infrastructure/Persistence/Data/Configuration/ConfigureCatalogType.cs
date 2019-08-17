using eWebShop.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eWebShop.Persistence.Data.Configuration
{
    public class ConfigureCatalogType : IEntityTypeConfiguration<CatalogType>
    {
        public void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogType");
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Id).ForSqlServerUseSequenceHiLo("catalog_type_hilo").IsRequired();
            builder.Property(cb => cb.Type).IsRequired().HasMaxLength(100);
        }
    }
}
