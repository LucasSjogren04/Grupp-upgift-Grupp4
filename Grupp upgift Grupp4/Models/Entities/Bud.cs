namespace Grupp_upgift_Grupp4.Models.Entities
{
    public class Bud
    {
        public int BidID { get; set; }
        public decimal BidAmount { get; set; }
        public int UserID { get; set; }
        public int AuctionID { get; set; }


        public Bud(int budId, decimal bidAmount, int userID, int auctionID)
        {
            BidID = budId;
            BidAmount = bidAmount;
            UserID = userID;
            AuctionID = auctionID;
        }
    }
}
