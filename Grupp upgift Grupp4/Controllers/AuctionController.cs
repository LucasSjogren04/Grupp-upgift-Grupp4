using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using Grupp_upgift_Grupp4.Repository.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Security.Claims;

namespace Grupp_upgift_Grupp4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionServices _auctionServices;

        public AuctionController(IAuctionServices auctionServices)
        {
            _auctionServices = auctionServices;
        }

        [HttpGet("GetAuctionByID")]
        public IActionResult GetAuctions(int auctionID)
        {
            var result = _auctionServices.GetAuctions(auctionID);

            if (result.searchedForAuction is Auctions searchedForAuction)
            {
                Auctions resultAuction = searchedForAuction;

                if (result.bidsOnAuction is List<Bid> bidsOnAuction)
                {
                    List<Bid> resultBidList = bidsOnAuction;
                    
                    string readResult =  (resultAuction.AuctionID.ToString() + " " + resultAuction.AuctionTitle + " " + 
                        resultAuction.AuctionDescription + " " + resultAuction.StartTime + " " + resultAuction.EndTime + " " +
                        resultAuction.StartBid + " " + resultAuction.)
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        
        [HttpPut("UpdateAuctions")]
        public IActionResult Update(Auctions auctions)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                string result = _auctionServices.UpdateAuction(auctions, username);

                if (result == "Auction Updated")
                {
                    return Ok(result);
                }
                else
                    return StatusCode(400, result);
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }
    }
}
