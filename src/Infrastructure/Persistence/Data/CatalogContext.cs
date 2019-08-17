using eWebShop.Application.Entities;
using eWebShop.Application.Entities.BasketAggregate;
using eWebShop.Application.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace eWebShop.Persistence.Data
{
    public class CatalogContext : DbContext
    {


        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }     
    }

}
