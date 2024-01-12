using Grupp_upgift_Grupp4.Repository.Interface;
using Microsoft.Data.SqlClient;

namespace Grupp_upgift_Grupp4.Repository.Repo
{
    public class DBContext : IDBContext
    {
        private readonly string? _connString;
        public DBContext(IConfiguration config)
        {
            _connString = config.GetConnectionString("DBConnection");

        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connString);

        }
    }
}
