using Dapper;
using DapperUnitOfWork.Models;
using DapperUnitOfWork.Repository.Interface;
using Microsoft.Data.SqlClient;
using System.Data;


namespace DapperUnitOfWork.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;


        public CustomerRepository(
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction
            )
        {
            _sqlConnection = sqlConnection;
            _dbTransaction = dbTransaction;
        }


        public async Task<int> InsertAsync(Customer entity)
        {
            var sql = @"INSERT INTO [Sales].[Customer]
                       ([PersonID],[StoreID],[TerritoryID],[rowguid])
                 VALUES(@PersonID,@StoreID,@TerritoryID,@rowguid)";

            var parameters = new DynamicParameters();
            parameters.Add("PersonID", entity.PersonID);
            parameters.Add("StoreID", entity.StoreID);
            parameters.Add("TerritoryID", entity.TerritoryID);
            parameters.Add("rowguid", entity.rowguid);

            return await _sqlConnection.ExecuteAsync(sql, parameters, _dbTransaction);

        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var sql = @"SELECT [CustomerID],[PersonID],[StoreID],[TerritoryID],[AccountNumber],[rowguid],[ModifiedDate] 
                        FROM [Sales].[Customer]";

            return await _sqlConnection.QueryAsync<Customer>(sql, null, _dbTransaction);
        }

        public async Task<Customer> FindAsync(int id)
        {
            var sql = @"SELECT [CustomerID],[PersonID],[StoreID],[TerritoryID],[AccountNumber],[rowguid],[ModifiedDate] 
                        FROM [Sales].[Customer] Where [CustomerID] = @CustomerID";

            var parameters = new DynamicParameters();
            parameters.Add("@CustomerID", id);

            return await _sqlConnection.QueryFirstAsync<Customer>(sql, parameters, _dbTransaction);

        }

        public async Task<int> UpdateAsync(Customer entity)
        {
            var sql = @"UPDATE [Sales].[Customer]
                        SET [PersonID] = @PersonID
                            ,[StoreID] = @StoreID
                            ,[TerritoryID] = @TerritoryID
                            ,[rowguid] = @rowguid
                            ,[ModifiedDate] = @ModifiedDate
                        WHERE [CustomerID] = @CustomerID";

            var parameters = new DynamicParameters();
            parameters.Add("PersonID", entity.PersonID);
            parameters.Add("StoreID", entity.StoreID);
            parameters.Add("TerritoryID", entity.TerritoryID);
            parameters.Add("rowguid", entity.rowguid);
            parameters.Add("ModifiedDate", entity.ModifiedDate);
            parameters.Add("@CustomerID", entity.CustomerID);

            //return await _sqlConnection.ExecuteAsync(sql, entity, _dbTransaction);
            return await _sqlConnection.ExecuteAsync(sql, parameters, _dbTransaction);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = @"DELETE FROM [Sales].[Customer]
                         WHERE [CustomerID] = @CustomerID";

            var parameters = new DynamicParameters();
            parameters.Add("@CustomerID", id);

            return await _sqlConnection.ExecuteAsync(sql, parameters, _dbTransaction);
        }

        public Task<bool> CustomMethod(int id)
        {
            throw new NotImplementedException();
        }
    }
}
