namespace NorthwindTest.Services
{
    public interface ICRUDService<T> where T : class
    {
        Task<T?> GetById(string id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> DeleteById(string id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
    }
}
