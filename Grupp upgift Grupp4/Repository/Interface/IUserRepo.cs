using Grupp_upgift_Grupp4.Models.Entities;

namespace Grupp_upgift_Grupp4.Repository.Interface
{
    public interface IUserRepo
    {
        User GetUser(string username);
        int GetUserID(string username);
        void Insert(User user);
        void Update(User user);
        void Delete(string username);
        Task<User> GetUserByUsernameAndPassword(string username, string password);
    }
}
