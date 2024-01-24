using Grupp_upgift_Grupp4.Repository.Interface;
using Grupp_upgift_Grupp4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Grupp_upgift_Grupp4.Models.Entities;
using System.Security.Claims;
using System;

namespace Grupp_upgift_Grupp4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidServices _BidServices;
        public BidController(IBidServices bidServices)
        {
            _BidServices = bidServices;
        }
        [HttpPost("InsertBid")]
        public IActionResult insertBid(Bid bids) 
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (username != null)
            {
                return Ok(_BidServices.InsertBid(bids, username));
            }
            else
            {
                return BadRequest();
            }
        }
    } 
    
    
}
