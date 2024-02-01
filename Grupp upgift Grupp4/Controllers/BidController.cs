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
        private readonly IBidServices _bidServices;
        public BidController(IBidServices bidServices)
        {
            _bidServices = bidServices;
        }
        [HttpPost("InsertBid")]
        public IActionResult InsertBid(Bid bids) 
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (username != null)
            {
                return Ok(_bidServices.InsertBid(bids, username));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteBid")]
        public IActionResult DeleteBid(int BidID) 
        { 
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (username != null) 
            {
                string result = (_bidServices.DeleteBid(BidID, username));
                if (result == "Bid deleted")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            {
                return BadRequest("Unathorized");
            }
        }
    } 
    
    
}
