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

        public AuctionController(IAuctionRepo auctionRepo, IHttpContextAccessor httpContextAccessor)
        {
            _auctionRepo = auctionRepo;
            _httpContextAccessor = httpContextAccessor;
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

        [HttpPost("insert")]
        public IActionResult PostAuction(Auctions auctions)
        {
            try
            {
                _auctionRepo.Insert(auctions);
                return Ok();
            }


            catch (Exception ex)
            {
                // Log the exception details (replace Console.WriteLine with your logging mechanism)
                Console.WriteLine($"An error occurred in GetAll: {ex.Message}");

                // Return HTTP 500 Internal Server Error with an error message
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
