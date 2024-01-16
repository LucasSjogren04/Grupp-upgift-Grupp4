using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using System.Security.Claims;

namespace Grupp_upgift_Grupp4.Repository.Repo
{
    public class TestGetClaim
    {

        private readonly IDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuctionRepo _auctionRepo;

        public TestGetClaim(IDBContext context, IHttpContextAccessor httpContextAccessor, IAuctionRepo auctionRepo)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _auctionRepo = auctionRepo;
        }

        private string GetUserID()
        {
            var userIDClaim = _httpContextAccessor.HttpContext.User.Identity.Name;

            return userIDClaim;
        }
    }
}
