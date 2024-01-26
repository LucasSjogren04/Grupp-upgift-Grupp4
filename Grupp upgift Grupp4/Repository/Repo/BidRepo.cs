using Dapper;
using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using Microsoft.AspNetCore.Http;

namespace Grupp_upgift_Grupp4.Repository.Repo
{
    public class BidRepo : IBidRepo
    {
        private readonly IDBContext _context;



        public BidRepo(IDBContext context)
        {
            _context = context;

        }
        public void InsertBid(Bid bids)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@BidAmount", bids.BidAmount);
                    parameters.Add("@UserID", bids.UserID);
                    parameters.Add("@AuctionID", bids.AuctionID);

                    db.Execute("InsertBid", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteBid(int BidID)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@BidID", BidID);
                    db.Execute("DeleteBid", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Bid GetBidbyID(int bid)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@BidID", bid);
                    
                  return db.QueryFirstOrDefault("GetBid", parameters, commandType: CommandType.StoredProcedure);
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
