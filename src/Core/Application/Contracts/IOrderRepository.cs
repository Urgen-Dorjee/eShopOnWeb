using eWebShop.Application.Entities.OrderAggregate;
using System.Threading.Tasks;

namespace eWebShop.Application.Contracts
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<Order> GetByIdWithItemAsync(int id);
    }
}
