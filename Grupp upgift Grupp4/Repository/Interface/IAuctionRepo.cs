using Grupp_upgift_Grupp4.Models.Entities;

namespace Grupp_upgift_Grupp4.Repository.Interface
{
    public interface IAuctionRepo
    {
        Auctions GetAcutionSearch(string auctionTitle);
        List<Auctions> GetAuctions();
        void Insert(Auctions auctions);
        void Update(Auctions auctions);
        void Delete(int auctionID );
    }
}
