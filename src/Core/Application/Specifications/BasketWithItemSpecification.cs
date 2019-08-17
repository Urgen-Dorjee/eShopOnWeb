using eWebShop.Application.Entities.BasketAggregate;

namespace eWebShop.Application.Specifications
{
    public sealed class BasketWithItemSpecification : BaseSpecification<Basket>
    {
        public BasketWithItemSpecification(int basketId) : base(b => b.Id == basketId)
        {
            AddInclude(b => b.Items);
        }
        public BasketWithItemSpecification(string buyer) : base(b => b.BuyerId == buyer)
        {
            AddInclude(b => b.Items); 
        }
    }
}
