using Dapper;
using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using System.Data;
using System.Security.Claims;
using Grupp_upgift_Grupp4.Repository.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.ModelConfiguration.Configuration;



namespace Grupp_upgift_Grupp4.Repository.Repo
{
    public class AuctionRepo : IAuctionRepo
    {

        private readonly IDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        public AuctionRepo(IDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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
        public string AddAuctionItem(string username, Auctions auctions)
        {
            using (IDbConnection db = _context.GetConnection())
            {

                var parameters = new DynamicParameters();
                parameters.Add("@UserName", username);
                parameters.Add("@AuctionTitle", auctions.AuctionTitle);
                parameters.Add("@AuctionDescription", auctions.AuctionDescription);
                parameters.Add("@StartTime", auctions.StartTime);
                parameters.Add("@EndTime", auctions.EndTime);
                parameters.Add("@StartBid", auctions.StartBid);
                parameters.Add("@ResultCode", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                db.Execute("AddAuctionItem", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<string>("@ResultCode");
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
