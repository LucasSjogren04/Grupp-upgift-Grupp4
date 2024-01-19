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
        public int GetUserID(string username)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters(); 
                    parameters.Add("@UserName", username);
                    var userID = db.Query("GetUserID", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    Console.WriteLine(userID);
                    string teststring = RemoveFirstAndLastCharacters(userID);
                    int reUserID = int.Parse(teststring);
                    Console.WriteLine(reUserID);
                    return reUserID;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string RemoveFirstAndLastCharacters(string input)
        {
            if (input.Length < 2)
            {
                // If the string has less than 2 characters, return an empty string or handle it as needed.
                return string.Empty;
            }

            // Use substring to remove the first and last characters.
            return input.Substring(1, input.Length - 2);
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

                    List<Bid> bidList = (List<Bid>)db.Query<Bid>("GetBidsByAuctionID", parameters, commandType: CommandType.StoredProcedure);
                    return bidList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public decimal GetStartBidByID(int auctionID)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@AuctionID", auctionID);

                    decimal startBid = db.Query("GetStartBidByID", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    return startBid;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
