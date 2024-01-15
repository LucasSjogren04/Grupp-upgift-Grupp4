using System.Text.Json.Serialization;

namespace Grupp_upgift_Grupp4.Models.Entities
{
    public class Auctions
    {
        [JsonIgnore]
        public int AuctionId { get; set; }
        public string AuctionTitle { get; set; }
        public string AuctionDescription { get; set; }
        public decimal Startbid{ get; set; }
        public DateOnly StartTid{ get; set;}
        public DateOnly SlutTid { get; set;}

        public Auctions(int auctionId, string auctionTitle, string auctionDescription, decimal startbid, DateOnly startTid, DateOnly slutTid)
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
