using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using System;
using System.Xml.Linq;

namespace Grupp_upgift_Grupp4.Services
{
    public class AuctionServices : IAuctionServices
    {
        private readonly IAuctionRepo _auctionRepo;
        private readonly IUserRepo _userRepo;

        public AuctionServices(IAuctionRepo auctionRepo, IUserRepo userRepo)
        {
            _auctionRepo = auctionRepo;
            _userRepo = userRepo;
        }

        //this is not used
        public (Auctions searchedForAuction, List<Bid> bidsOnAuction) GetAuctions(int auctionID)
        {
            Auctions searchedForAuction = _auctionRepo.GetAuctionByAuctionID(auctionID);
            List<Bid> bidsOnAuction = _auctionRepo.GetBidsByAuctionID(auctionID);

            //If the acution is still open
            if (searchedForAuction.EndTime > DateTime.Now)
            {
                return (searchedForAuction, bidsOnAuction);
            }
            //If the Auction is closed
            else
            {
                //Returns the highest bid (if there is one)
                Bid maxBid = bidsOnAuction.OrderByDescending(obj => obj.BidAmount).FirstOrDefault();
                if (maxBid != null)
                {
                    List<Bid> topBid = [maxBid];
                    return (searchedForAuction, topBid);
                }
                return (searchedForAuction, null);
            }
        }

        //this is used
        public string GetAuctions2(int auctionID)
        {
            Auctions searchedForAuction = _auctionRepo.GetAuctionByAuctionID(auctionID);
            List<Bid> bidsOnAuction = _auctionRepo.GetBidsByAuctionID(auctionID);

            if (searchedForAuction != null)
            {
                string result = ("Searched For Auction: " + searchedForAuction.AuctionID + "\n" +
                        searchedForAuction.AuctionTitle + "\n" +
                        searchedForAuction.AuctionDescription + "\n" +
                        searchedForAuction.StartTime + "\n" +
                        searchedForAuction.EndTime + "\n" +
                        searchedForAuction.StartBid + "\n" +
                        searchedForAuction.UserID + "\n\n" +
                        "Bids on auction: " + "\n\n");
                
                if (bidsOnAuction.Count != 0 && searchedForAuction.EndTime > DateTime.Now)
                {
                    foreach (Bid bid in bidsOnAuction)
                    {
                        result += ("Amount: " + bid.BidAmount + "\n"+
                            "UserID: " + bid.UserID +"\n");
                    }
                    return result;
                }
                if(bidsOnAuction.Count != 0 && searchedForAuction.EndTime < DateTime.Now)
                {
                    Bid maxBid = bidsOnAuction.OrderByDescending(obj => obj.BidAmount).FirstOrDefault();
                    result += ("Highest bid on auction: \n" + "Amount: " + maxBid.BidAmount + "\n"+
                       "UserID: " + maxBid.UserID);
                    return result;
                }
            }
            return "That auction doesn't exist";

        }

        public string AddAuction(Auctions auctions, string username)
        {
            int UserID = _userRepo.GetUserID(username);

            if (UserID != 0)
            {
                auctions.UserID = UserID;
                _auctionRepo.InsertAuction(auctions);
                return "Auction inserted";
            }
            else
            {
                return "You need to be logged in to insert an auction";
            }
        }
        public string DeleteAuction(int auctionID, string username)
        {
            List<Bid> bidsOnAuction = _auctionRepo.GetBidsByAuctionID(auctionID);
            if (bidsOnAuction.Count == 0)
            {
                int UserID = _userRepo.GetUserID(username);

                if (UserID != 0)
                {
                    List<Auctions> userOwnedAuctions = _auctionRepo.GetLoggedInUsersAuctions(UserID);
                    bool userOwnsAuction = userOwnedAuctions.Any(uOA => uOA.AuctionID == auctionID);
                    if (userOwnsAuction == true)
                    {
                        _auctionRepo.DeleteAuction(auctionID);
                        return "Auction deleted.";
                    }
                    return "You cannot delete an Auction that you do not own.";
                }
                return "You have to be logged in to delete your auction";
            }
            return "You cannot delete an auction that has been bidded on";
        }


        public string UpdateAuction(Auctions auctions, string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                int UserID = _userRepo.GetUserID(username);

                if (UserID != 0)
                {
                    auctions.UserID = UserID;
                    List<Auctions> userOwnedAuctions = _auctionRepo.GetLoggedInUsersAuctions(UserID);
                    bool userOwnsAuction = userOwnedAuctions.Any(uOA => uOA.AuctionID == auctions.AuctionID);
                    if (userOwnsAuction == true)
                    {
                        int auctionID = auctions.AuctionID;
                        List<Bid> bidList = _auctionRepo.GetBidsByAuctionID(auctionID);
                        if (bidList.Count == 0)
                        {
                            _auctionRepo.UpdateAuction(auctions);
                            return "Auction Updated";
                        }
                        else
                        {
                            Auctions auctionForUpdate = _auctionRepo.GetAuctionByAuctionID(auctionID);
                            if (auctionForUpdate.StartBid == auctions.StartBid)
                            {
                                _auctionRepo.UpdateAuction(auctions);
                                return "Auction Updated";
                            }
                            return "You cannot change the Start bid for this auction because it has already been bidded on.";
                        }

                    }
                    return "You don't own that auction";
                }
                return "You do not own that auction";
            }
            return "Your login credentials are not valid.";
        }

    }
}


