using Grupp_upgift_Grupp4.Models.Entities;

namespace Grupp_upgift_Grupp4.Repository.Interface
{
    public interface IUserRepo
    {
        User GetUser(string username);
        void Insert(User user);
        void Update(User user);
        void Delete(string username);
        string AddAuctionItem(string userName, Auctions auctions);
        Task<User> GetUserByUsernameAndPassword(string username, string password);
    }
}
