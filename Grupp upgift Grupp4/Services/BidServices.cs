using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using Grupp_upgift_Grupp4.Repository.Repo;
using System;
using System.Xml.Linq;
namespace Grupp_upgift_Grupp4.Services
{

    public class BidServices : IBidServices
    {
        /*Den användare som skapat auktionen kan inte
          själv lägga bud på den.
        Ett bud har ett pris och är kopplat till användare = UserID

        Det går även att ångra dvs ta bort ett bud om auktionen inte är avslutad
        Är auktionen fortfarande öppen kan användaren lägga ett bud.
        Detta måste vara högre än det tidigare högsta budet,
        */
        /* Hämta Lista på tidigare bud kolla om det är högre än dom tidigare buden */
        
        private readonly IAuctionRepo _auctionRepo;
        private readonly IBidRepo _bidRepo;
        private readonly IUserRepo _userRepo;

        public BidServices(IAuctionRepo auctionRepo, IBidRepo bidRepo, IUserRepo userRepo)
        {
            _auctionRepo = auctionRepo;
            _bidRepo = bidRepo;
            _userRepo = userRepo;
        }
        public string InsertBid(Bid bids, string username)
        {
            int UserID = _userRepo.GetUserID(username);

            if (UserID != 0)
            {
                List<Bid> bidList = _auctionRepo.GetBidsByAuctionID(bids.AuctionID);
                Bid maxBid = bidList.OrderByDescending(obj => obj.BidAmount).FirstOrDefault();
                bids.UserID = UserID;
                if(bids.BidAmount > maxBid.BidAmount )
                {
                    _bidRepo.InsertBid(bids);
                    return "Bid Inserted";
                }
                else 
                {
                    return "New bid must be higher than the last bid";
                }
            }
            else
            {
                return "You need to be logged in to insert a bid";
            }
        }
        /*Kolla om Auktionen inte är stängd isåfall ska det gå att ta ångra sitt bud*/
        public string DeleteBid() 
        {
            return null;   
        }
        
    }
}

