using Ardalis.GuardClauses;
using eWebShop.Application.Entities.BasketAggregate;

namespace eWebShop.Application.Exceptions
{
    public static class GuardExtensions
    {
        public static void NullBasket(this IGuardClause guardClause, int basketId, Basket basket)
        {
            if (basket == null)
            {
                throw new BasketNotFound(basketId);
            }
        }
    }
}
