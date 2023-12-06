using System.Data;
using DapperUnitOfWork.Repository.Interface;

namespace DapperUnitOfWork.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbTransaction _dbTransaction;
        public ICustomerRepository CustomerRepository { get; }

        public UnitOfWork(
            IDbTransaction dbTransaction,
            ICustomerRepository customerRepository
            )
        {
            _dbTransaction = dbTransaction;
            CustomerRepository = customerRepository;
        }

        public void SaveChanges()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
    }
}
