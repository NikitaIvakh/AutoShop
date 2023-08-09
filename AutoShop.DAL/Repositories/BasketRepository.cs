using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;

namespace AutoShop.DAL.Repositories
{
    public class BasketRepository : IBaseRepository<Basket>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BasketRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task CreateAsync(Basket entity)
        {
            await _applicationDbContext.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public IQueryable<Basket> GetAllElements()
        {
            return _applicationDbContext.Baskets;
        }

        public async Task<Basket> UpdateAsync(Basket type)
        {
            _applicationDbContext.Baskets.Update(type);
            await _applicationDbContext.SaveChangesAsync();

            return type;
        }

        public async Task DeleteAsync(Basket entity)
        {
            _applicationDbContext.Baskets.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}