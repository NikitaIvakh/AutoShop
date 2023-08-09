using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;

namespace AutoShop.DAL.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OrderRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task CreateAsync(Order entity)
        {
            await _applicationDbContext.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public IQueryable<Order> GetAllElements()
        {
            return _applicationDbContext.Orders;
        }

        public async Task<Order> UpdateAsync(Order type)
        {
            _applicationDbContext.Orders.Update(type);
            await _applicationDbContext.SaveChangesAsync();

            return type;
        }

        public async Task DeleteAsync(Order entity)
        {
            _applicationDbContext.Orders.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}