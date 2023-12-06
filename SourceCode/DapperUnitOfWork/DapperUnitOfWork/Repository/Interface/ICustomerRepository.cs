using DapperUnitOfWork.Models;

namespace DapperUnitOfWork.Repository.Interface
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<bool> CustomMethod(int id);
    }
}
