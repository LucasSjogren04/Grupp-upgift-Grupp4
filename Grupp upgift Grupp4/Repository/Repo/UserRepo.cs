using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using System.Data;

namespace Grupp_upgift_Grupp4.Repository.Repo
{
    public class UserRepo :IUserRepo
    {
        private readonly IDBContext _context;

        public UserRepo(IDBContext context)
        {
            _context = context;
        }

        public List<User> GetAllUser()
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    return db.Query<User>("ShowAllUsers", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception details (replace Console.WriteLine with your logging mechanism)
                Console.WriteLine($"An error occurred in GetAllUsers: {ex.Message}");
                throw;
            }
        }
    }
}
