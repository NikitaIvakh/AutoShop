using AutoShop.Domain.ViewModels.Car;

namespace AutoShop.DAL.Interfaces
{
    public interface IBaseRepository<Type>
    {
        Task<bool> CreateAsync(Type entity);

        Task<Type> GetAsync(int id);

        Task<IEnumerable<Type>> SelectAsync();

        Task<Type> UpdateElementAsync(Type type);

        Task<bool> DeleteAsync(Type entity);
    }
}