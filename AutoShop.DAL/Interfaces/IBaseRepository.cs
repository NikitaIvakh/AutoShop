namespace AutoShop.DAL.Interfaces
{
    public interface IBaseRepository<Type>
    {
        Task CreateAsync(Type entity);

        IQueryable<Type> GetAllElements();

        Task<Type> UpdateAsync(Type type);

        Task DeleteAsync(Type entity);
    }
}