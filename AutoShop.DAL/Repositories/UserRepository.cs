using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;

namespace AutoShop.DAL.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task CreateAsync(User entity)
        {
            await _applicationDbContext.Users.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public IQueryable<User> GetAllAsync()
        {
            return _applicationDbContext.Users;
        }
        public async Task<User> UpdateAsync(User type)
        {
            _applicationDbContext.Users.Update(type);
            await _applicationDbContext.SaveChangesAsync();

            return type;
        }

        public async Task DeleteAsync(User entity)
        {
            _applicationDbContext.Users.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}