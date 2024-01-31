using Dapper;
using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using System.Data;
using System.Security.Claims;
using Grupp_upgift_Grupp4.Repository.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Infrastructure;



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
        public Auctions GetAuctionByID(int auctionID)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                var parameter = new DynamicParameters();
                parameter.Add("@AuctionID", auctionID);
                return (Auctions)db.Query("GetAuctionByID", commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateAuction(Auctions auctions)
        {
            using (IDbConnection db = _context.GetConnection())
            {

                var parameters = new DynamicParameters();
                parameters.Add("@AuctionID", auctions.AuctionID);
                parameters.Add("@AuctionTitle", auctions.AuctionTitle);
                parameters.Add("@AuctionDescription", auctions.AuctionDescription);
                parameters.Add("@StartTime", auctions.StartTime);
                parameters.Add("@EndTime", auctions.EndTime);
                parameters.Add("@StartBid", auctions.StartBid);

                db.Execute("UpdateAuction", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public List<Auctions> GetLoggedInUsersAuctions(int UserID)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@UserID", UserID);

                    List<Auctions> userOwnedAuctions = (List<Auctions>)db.Query<Auctions>("GetLoggedInUsersAuctions", parameters, commandType: CommandType.StoredProcedure);
                    
                    return userOwnedAuctions;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Bid> GetBidsByAuctionID(int auctionID)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection()) 
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@AuctionID", auctionID);

                    return db.Query<Bid>("GetBidsByAuctionID", parameters, commandType: CommandType.StoredProcedure).ToList();
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Auctions GetAuctionByAuctionID(int auctionID)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@AuctionID", auctionID);

                    Auctions searchedForAuction = db.QueryFirstOrDefault<Auctions>("GetAuctionByID", parameters, commandType: CommandType.StoredProcedure);
                    Console.WriteLine(searchedForAuction.AuctionTitle);
                    return searchedForAuction;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public void InsertAuction(Auctions auctions)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@AuctionTitle", auctions.AuctionTitle);
                    parameters.Add("@AuctionDescription", auctions.AuctionDescription);
                    parameters.Add("@StartTime", auctions.StartTime);
                    parameters.Add("@EndTime", auctions.EndTime);
                    parameters.Add("@StartBid", auctions.StartBid);
                    parameters.Add("@UserID", auctions.UserID);
                    db.Execute("InsertAuction", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteAuction(int auctionID)   
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@AuctionID", auctionID);
                    db.Execute("DeleteAuction", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }        
    }
}
