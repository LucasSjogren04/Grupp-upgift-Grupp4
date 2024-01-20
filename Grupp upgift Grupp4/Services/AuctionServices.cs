using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using System;
using System.Xml.Linq;

namespace Grupp_upgift_Grupp4.Services
{
    public class AuctionServices: IAuctionServices
    {
        private readonly IAuctionRepo _auctionRepo;
        private readonly IUserRepo _userRepo;

        public AuctionServices(IAuctionRepo auctionRepo, IUserRepo userRepo)
        {
            _auctionRepo = auctionRepo;
            _userRepo = userRepo;
        }

        
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
                List<Bid> topBid = [maxBid];
                return (searchedForAuction, topBid);
            }
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
                    foreach (Auctions auction in userOwnedAuctions)
                    {
                        if (auction.AuctionID == auctionID)
                        {
                            _auctionRepo.DeleteAuction(auctionID);
                            return "Auction deleted.";
                        }
                        else
                        {
                            return "You cannot delete an Auction that you do not own.";
                        }
                    }
                    return "That auction doesn't exist";
                }
                else
                {
                    return "You must be logged in to delete an auction.";
                }
            }
            else
            {
                return "You cannot delete an Auction which has been bidded on";
            }   
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
                    foreach (Auctions auctionsInList in userOwnedAuctions)
                    {
                        if (auctionsInList.AuctionID == auctions.AuctionID)
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
                                else
                                {
                                    return "You cannot change the Start bid for this auction because it has already been bidded on.";
                                }
                            }
                        }
                        else
                        {
                            return "You do not own that auction";
                        }
                    }
                    return "You don't have any auctions Listed";
                    
                }
                else
                {
                    return "Your login credentials are not valid.";
                }
            }
            else
            {
                return "You have to login to update your auctions.";
            }
        }       
    }
}
