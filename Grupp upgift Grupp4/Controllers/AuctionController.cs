using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using Grupp_upgift_Grupp4.Repository.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        //[HttpGet("show")]
        //public IActionResult GetAuction()
        //{
        //    try
        //    {
        //        var result = _auctionRepo.GetAuctions();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpPost("AddAuctions")]
        //public IActionResult Create(Auctions auctions)
        //{
        //    try
        //    {
        //        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        //        var result = _userRepo.GetUser(username);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        
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
