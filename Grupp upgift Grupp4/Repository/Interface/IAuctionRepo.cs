using Grupp_upgift_Grupp4.Models.Entities;

namespace Grupp_upgift_Grupp4.Repository.Interface
{
    public interface IAuctionRepo
    {
        List<Auctions> GetAuctions();
        //string AddAuctionItem(Auctions auctions);
        Auctions GetAuctionByAuctionID(int auctionID);
        void UpdateAuction(Auctions auctions);
        List<Auctions> GetLoggedInUsersAuctions(int UserID);
        List<Bid> GetBidsByAuctionID(int auctionID);
        string Delete(int auctionID );
    }
}
