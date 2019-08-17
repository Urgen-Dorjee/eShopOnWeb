using eWebShop.Application.Entities.OrderAggregate;
using System.Threading.Tasks;

namespace eWebShop.Application.Contracts
{
    public interface IOrderService
    {
        Task CreateOrderAsync(int basketId, Address shippingAddress);
    }
}
