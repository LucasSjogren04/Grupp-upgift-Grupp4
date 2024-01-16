using System.Text.Json.Serialization;

namespace Grupp_upgift_Grupp4.Models.Entities
{
    public class Auctions
    {
        [JsonIgnore]
        public int AuctionID { get; set; }
        public string AuctionTitle { get; set; }
        public string AuctionDescription { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }
        public decimal Startbud { get; set; }
        public int UserID { get; set; }

        public Auctions(int auctionId, string auctionTitle, string auctionDescription, decimal startbud, DateTime startTid, DateTime slutTid,int userID)
        {
            AuctionID = auctionId;
            AuctionTitle = auctionTitle;
            AuctionDescription = auctionDescription;
            Startbud = startbud;
            StartTid = startTid;
            SlutTid = slutTid;
            UserID = userID;
        }

        public Auctions() { }
    }
}
