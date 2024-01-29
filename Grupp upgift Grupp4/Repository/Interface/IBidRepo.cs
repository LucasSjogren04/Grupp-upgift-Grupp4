using Grupp_upgift_Grupp4.Models.Entities;

namespace Grupp_upgift_Grupp4.Repository.Interface
{
    public interface IBidRepo
    {
        void InsertBid(Bid bids);
        void DeleteBid(int BidID);
        Bid GetBidbyID(int BidID);
    }
}
