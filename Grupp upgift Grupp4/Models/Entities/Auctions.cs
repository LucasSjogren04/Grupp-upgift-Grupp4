namespace Grupp_upgift_Grupp4.Models.Entities
{
    public class Auctions
    {
        public int AuctionId { get; set; }
        public string AuctionTitle { get; set; }
        public string AuctionDescription { get; set; }
        public decimal Startbid{ get; set; }
        public DateTime StartTid{ get; set;}
        public DateTime SlutTid { get; set;}

        public Auctions(int auctionId, string auctionTitle, string auctionDescription, decimal startbid, DateTime startTid, DateTime slutTid)
        {
            AuctionId = auctionId;
            AuctionTitle = auctionTitle;
            AuctionDescription = auctionDescription;
            Startbid = startbid;
            StartTid = startTid;
            SlutTid = slutTid;
        }
    }
}
