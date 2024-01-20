using Grupp_upgift_Grupp4.Models.Entities;

namespace Grupp_upgift_Grupp4.Repository.Interface
{
    public interface IAuctionServices
    {
        string UpdateAuction(Auctions auctions, string username);
        (Auctions searchedForAuction, List<Bid> bidsOnAuction) GetAuctions(int auctionID);
        string DeleteAuction(int auctionID, string username);
        string AddAuction(Auctions auctions, string username);


        //ignore these comments <--------------------- READ THIS





        //Gets the userID so that we can use it for semthing else


        //Gets a list of Auctions with the same UserID as the Logged in User.
        //It then checks if the entered AuctionID is present in the list.
        //If it is not present in the list it will return an error message saying:
        //"You do not have permission to update this auction"

        //If it is present in the list it will call "GetBidsOnAuction(auction)" which returns a count of bids.
        //If the list of bids is NOT 0 it will call "ComparePrice(auction)" which checks if StartBid is changed.
        //If the StartBid is changed it will return an error message saying:
        //"You cannot change the StartBid after your auction has been bidded on."

        //If the list of bids is 0 it will Send the Auction object to AuctionRep for updating



        //returns a list of auctions


    }
}
