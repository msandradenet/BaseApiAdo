using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace BaseApiAdo.INFRA
{
    public class SqlAdapter
    {
        public SqlConnection _sqlConnection { get; set; }
        public SqlTransaction _sqlTransaction { get; set; }

        public void OpenConnection(IConfiguration _configuration)
        {
            _sqlConnection = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value);

            _sqlConnection.Open();            
        }

        public void OpenTransaction()
        {
            _sqlTransaction = _sqlConnection.BeginTransaction();
        }

        public void Dispose()
        {
            if (_sqlTransaction != null)
            {
                _sqlTransaction.Dispose();
            }

            if (_sqlConnection != null)
            {
                _sqlConnection.Close();
                _sqlConnection.Dispose();
            }
        }

        public void SaveChanges()
        {
            _sqlTransaction.Commit();
        }
    }
}
