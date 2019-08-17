using eWebShop.Application.Entities.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eWebShop.Persistence.Data.Configuration
{
    public class ConfigureBasketItem : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.Property(bi => bi.UnitPrice).IsRequired(true).HasColumnType("decimal(18,2)");
        }
    }
}
