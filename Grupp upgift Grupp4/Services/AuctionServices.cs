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

            if (searchedForAuction.EndTime < DateTime.Now)
            {
                return (searchedForAuction, bidsOnAuction);  
            }
            else
            {
                Bid maxBid = bidsOnAuction.OrderByDescending(obj => obj.BidAmount).FirstOrDefault();
                List<Bid> topBid = [maxBid];
                return (searchedForAuction, topBid);
            }
        }

        public string UpdateAuction(Auctions auctions, string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                int UserID = _userRepo.GetUserID(username);

                if (UserID != 0)
                {  
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
