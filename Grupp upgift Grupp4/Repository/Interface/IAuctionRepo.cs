using Grupp_upgift_Grupp4.Models.Entities;

namespace Grupp_upgift_Grupp4.Repository.Interface
{
    public interface IAuctionRepo
    {
        List<Auctions> GetAuctions();
        int GetUserID(string username);
        //string AddAuctionItem(Auctions auctions);
        void UpdateAuction(Auctions auctions);
        List<Auctions> GetLoggedInUsersAuctions(int UserID);
        List<Bid> GetBidsByAuctionID(int auctionID);
        decimal GetStartBidByID(int auctionID);
        Auctions GetAcutionSearch(string auctionTitle);
        void Delete(int auctionID );
    }
}
