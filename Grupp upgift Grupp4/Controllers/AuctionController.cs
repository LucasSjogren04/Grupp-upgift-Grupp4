using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using Grupp_upgift_Grupp4.Repository.Repo;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("insert")]
        public IActionResult PostAuction(Auctions auctions)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            _auctionRepo.Insert(auctions, username);
            if (user.UserName == username)
            {
                return Ok("User update Successfully");
            }
            else
                return BadRequest();
        }

    }
}
