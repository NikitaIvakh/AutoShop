using AutoShop.DAL.Interfaces;
using AutoShop.Domain.Entity;

namespace AutoShop.DAL.Repositories
{
    public class ProfileRepository : IBaseRepository<Profile>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProfileRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task CreateAsync(Profile entity)
        {
            await _applicationDbContext.Profiles.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public IQueryable<Profile> GetAllElements()
        {
            return _applicationDbContext.Profiles;
        }

        public async Task<Profile> UpdateAsync(Profile type)
        {
            _applicationDbContext.Profiles.Update(type);
            await _applicationDbContext.SaveChangesAsync();

            return type;
        }

        public async Task DeleteAsync(Profile entity)
        {
            _applicationDbContext.Profiles.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}