namespace AutoShop.DAL.Interfaces
{
    public interface IBaseRepository<Type>
    {
        Task CreateAsync(Type entity);

        IQueryable<Type> GetAllAsync();

        Task<Type> UpdateAsync(Type type);

        Task DeleteAsync(Type entity);
    }
}