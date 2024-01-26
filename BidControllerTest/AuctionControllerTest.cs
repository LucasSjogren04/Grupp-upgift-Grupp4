using Moq;
using Microsoft.AspNetCore.Http;
using Grupp_upgift_Grupp4.Services;
using Grupp_upgift_Grupp4.Repository.Interface;
namespace BidControllerTest
{
    public class AuctionControllerTest
    {
        [Fact]
        public void Test_Auction_Controller()
        {
            Mock auctionRepo = new Mock<IAuctionRepo>();
            auctionRepo.Setup(repo => repo.DeleteAuction(It.IsAny<int>()))
                         .ReturnsAsync((string username, string password) =>
    }
}