using eWebShop.Application.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eWebShop.Persistence.Data.Configuration
{
    public class ConfigureOrderItem : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdered);
            builder.Property(oi => oi.UnitPrice).IsRequired(true).HasColumnType("decimal(18,2)");
        }
    }
}
