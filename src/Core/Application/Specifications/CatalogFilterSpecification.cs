using eWebShop.Application.Entities;

namespace eWebShop.Application.Specifications
{
    public class CatalogFilterSpecification : BaseSpecification<CatalogItem>
    {
        public CatalogFilterSpecification(int? brandId, int? typeId)
            : base(i => (!brandId.HasValue || i.CatalogBrandId == brandId) && (!typeId.HasValue || i.CatalogTypeId == typeId))
        { }
    }
}
