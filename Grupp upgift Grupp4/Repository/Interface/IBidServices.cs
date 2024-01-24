using Grupp_upgift_Grupp4.Models.Entities;

namespace Grupp_upgift_Grupp4.Repository.Interface
{
    public interface IBidServices
    {
        string InsertBid(Bid bids, string username);
        string DeleteBid();
    }
}
