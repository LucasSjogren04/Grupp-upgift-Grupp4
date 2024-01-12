using Microsoft.Data.SqlClient;

namespace Grupp_upgift_Grupp4.Repository.Interface
{
    public interface IDBContext
    {
        SqlConnection GetConnection();
    }
}
