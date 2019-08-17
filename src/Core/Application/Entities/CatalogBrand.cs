using eWebShop.Application.Contracts;

namespace eWebShop.Application.Entities
{
    public class CatalogBrand : BaseEntity, IAggregateRoot
    {
        public string Brand { get; set; }
    }
}
