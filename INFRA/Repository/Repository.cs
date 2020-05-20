using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BaseApiAdo.INFRA.Repository
{
    public class Repository
    {
        protected SqlConnection _sqlConnection;
        protected SqlTransaction _sqlTransaction;

        protected SqlCommand CreateCommand(string query)
        {
            return new SqlCommand(query, _sqlConnection, _sqlTransaction);
        }
    }
}
