namespace AutoShop.DAL.Interfaces
{
    public interface IBaseRepository<Type>
    {
        bool Create(Type entity);

        Type Get(int Id);

        Task<IEnumerable<Type>> Select();

        bool Delete(Type entity);
    }
}