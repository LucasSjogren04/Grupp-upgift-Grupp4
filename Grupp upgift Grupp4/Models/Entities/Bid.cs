namespace Grupp_upgift_Grupp4.Models.Entities
{
    public class Bid
    {
        public int BidID { get; set; }
        public decimal BidAmount { get; set; }
        public int UserID { get; set; }
        public int AuctionID { get; set; }


        public Bid(int bidID, decimal bidAmount, int userID, int auctionID)
        {
            BidID = bidID;
            BidAmount = bidAmount;
            UserID = userID;
            AuctionID = auctionID;
        }
    }
}
