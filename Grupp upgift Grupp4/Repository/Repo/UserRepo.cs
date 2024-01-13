using Dapper;
using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using System.Data;
using System.Data.Common;

namespace Grupp_upgift_Grupp4.Repository.Repo
{
    public class UserRepo :IUserRepo
    {
        private readonly IDBContext _context;

        public UserRepo(IDBContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            // Implement the logic to fetch the user from the database based on the username and password

            using (var db = _context.GetConnection())
            {
                var user = await db.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password",
                    new { UserName = username, Password = password });

                return user;
            }
        }

        public User GetUser(string username)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                var parameters = new DynamicParameters();
                    parameters.Add("@UserName", username);
                    return db.QueryFirstOrDefault<User>("GetUserInfo", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                // Log the exception details (replace Console.WriteLine with your logging mechanism)
                Console.WriteLine($"An error occurred in Get User Info: {ex.Message}");
                throw ex;
            }
        }

        public void Insert(User user)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    //parameters.Add("@UserId", user.UserId);
                    parameters.Add("@UserName", user.UserName);
                    parameters.Add("@Password", user.Password);
                    parameters.Add("@FirstName", user.FirstName);
                    parameters.Add("@LastName", user.LastName);
                    db.Execute("AddUser", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in InsertUser: {ex.Message}");

            }
        }

        public void Update(User user)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@UserName", user.UserName);
                    parameters.Add("@Password", user.Password);
                    parameters.Add("@FirstName", user.FirstName);
                    parameters.Add("@LastName", user.LastName);
                    
                    db.Execute("UpdateUser", parameters, commandType: CommandType.StoredProcedure);
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in InsertUser: {ex.Message}");
             

            }

        }
        public void Delete(string username)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@UserName", username);

                    db.Execute("DeleteUser", parameters, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Delete: {ex.Message}");

            }
        }
    }
}
