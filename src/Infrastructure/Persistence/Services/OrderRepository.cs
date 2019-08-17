using eWebShop.Application.Contracts;
using eWebShop.Application.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using eWebShop.Persistence.Data;
using System.Threading.Tasks;

namespace eWebShop.Persistence.Services
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(CatalogContext context) : base(context)
        { }

        public Task<Order> GetByIdWithItemAsync(int id)
        {
            return _dbContext.Orders.Include(o => o.OrderItems)
                   .Include($"{ nameof(Order.OrderItems)}.{ nameof(OrderItem.ItemOrdered)}")
                   .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
