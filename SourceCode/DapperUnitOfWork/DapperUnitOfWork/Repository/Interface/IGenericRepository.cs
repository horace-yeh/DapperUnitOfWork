namespace DapperUnitOfWork.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<int> InsertAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> FindAsync(int id);

        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(int id);
    }
}
