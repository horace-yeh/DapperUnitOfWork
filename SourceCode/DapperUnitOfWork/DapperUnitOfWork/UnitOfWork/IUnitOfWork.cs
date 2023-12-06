using DapperUnitOfWork.Repository.Interface;

namespace DapperUnitOfWork.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }

        void SaveChanges();
    }
}
