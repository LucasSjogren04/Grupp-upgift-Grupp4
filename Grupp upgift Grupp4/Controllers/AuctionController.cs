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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuctionRepo _auctionRepo;
        private readonly IUserRepo _userRepo;

        public AuctionController(IAuctionRepo auctionRepo, IHttpContextAccessor httpContextAccessor, IUserRepo userRepo)
        {
            _auctionRepo = auctionRepo;
            _httpContextAccessor = httpContextAccessor;
            _userRepo = userRepo;
        }

        [HttpGet("show")]
        public IActionResult GetAuction()
        {
            try
            {
                var result = _auctionRepo.GetAuctions();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("AddAuctions")]
        public IActionResult Create(Auctions auctions)
        {
            try
            {
                //this sends users to a SP which handels the authentication: BAD!
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                string auction = _auctionRepo.AddAuctionItem(username, auctions);

                if (auction == "Auction item added successfully")
                {
                    return Ok(auction);
                }
                else
                    return StatusCode(400, auction);
                // If successful, return status code 201



            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return an appropriate error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
        
        [HttpPut("UpdateAuctions")]
        public IActionResult Update(Auctions auctions)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                string auction = _auctionRepo.Update(username, auctions);

                if (auction == "Auction item added successfully")
                {
                    return Ok(auction);
                }
                else
                    return StatusCode(400, auction);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }
    }
}
