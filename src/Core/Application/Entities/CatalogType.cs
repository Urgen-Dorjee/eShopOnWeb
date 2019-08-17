using eWebShop.Application.Contracts;

namespace eWebShop.Application.Entities
{
    public class CatalogType : BaseEntity, IAggregateRoot
    {
        public string Type { get; set; }
    }
}
