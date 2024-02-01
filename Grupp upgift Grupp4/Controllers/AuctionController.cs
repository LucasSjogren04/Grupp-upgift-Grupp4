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
            string result = _auctionServices.GetAuctions2(auctionID);

            if(result.Contains("Searched For Auction: ")) 
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddAuction")]
        public IActionResult AddAuction(Auctions auctions)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (username != null)
            {
                return Ok(_auctionServices.AddAuction(auctions, username));
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
                }return BadRequest(result);
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }
        [HttpDelete("DeleteAuction")]
        public IActionResult Delete(int auctionID)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                if (username != null)
                {
                    return Ok(_auctionServices.DeleteAuction(auctionID, username));
                }
                return BadRequest();
            }
            catch { return BadRequest(); }
        }
    }
}
