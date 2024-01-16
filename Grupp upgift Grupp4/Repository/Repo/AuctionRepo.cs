using Dapper;
using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using System.Data;
using System.Security.Claims;
using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using Grupp_upgift_Grupp4.Repository.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Security.Claims;
using static Dapper.SqlMapper;


namespace Grupp_upgift_Grupp4.Repository.Repo
{
    public class AuctionRepo : IAuctionRepo
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuctionRepo _auctionRepo;
        private readonly IUserRepo _userRepo;
        private readonly IDBContext _context;

        public AuctionRepo(IAuctionRepo auctionRepo, IHttpContextAccessor httpContextAccessor, IUserRepo userRepo, IDBContext context)
        {
            _auctionRepo = auctionRepo;
            _httpContextAccessor = httpContextAccessor;
            _userRepo = userRepo;
            _context = context;
        }
        public List<Auctions> GetAuctions()
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    return (List<Auctions>)db.Query<Auctions>("GetAllAuctions", commandType: CommandType.StoredProcedure);
                }
            }

            catch (Exception ex)
            {   
                throw ex;
            }
        }
        public void Insert(Auctions auctions)
        {

            var username = identity.Claims.Where(c => c.Type == ClaimTypes.Name)
                   .Select(c => c.Value).SingleOrDefault();
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    //parameters.Add("@UserId", user.UserId);
                    parameters.Add("@AuctionTitle", auctions.AuctionTitle);
                    parameters.Add("@AuctionDescription", auctions.AuctionDescription);
                    parameters.Add("@StartTid", auctions.StartTid);
                    parameters.Add("@SlutTid", auctions.SlutTid);
                    parameters.Add("@Startbud", auctions.Startbud);
                    parameters.Add("@UserID", username);
                    db.Execute("AddUser", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in InsertUser: {ex.Message}");

            }
        }
        public void Update(Auctions auctions)
        {
            throw new NotImplementedException();
        }
        public void Delete(int auctionID)
        {
            throw new NotImplementedException();
        }
        public Auctions GetAcutionSearch(string auctionTitle)
        {
            throw new NotImplementedException();
        }

    }
}
