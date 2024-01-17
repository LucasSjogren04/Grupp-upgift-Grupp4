using System.Text.Json.Serialization;

namespace Grupp_upgift_Grupp4.Models.Entities
{
    public class Auctions
    {
        //        [JsonIgnore]
        public int AuctionID { get; set; } = 0;
        public string AuctionTitle { get; set; }
        public string AuctionDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal StartBid { get; set; }

        public Auctions(int auctionId, string auctionTitle, string auctionDescription, DateTime startTime, DateTime endTime, decimal startBid)
        {
            AuctionID = auctionId;
            AuctionTitle = auctionTitle;
            AuctionDescription = auctionDescription;           
            StartTime = startTime;
            EndTime = endTime;
            StartBid = startBid;
        }

        public Auctions() { }
    }
}
