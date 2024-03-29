﻿using FakeItEasy;
using Grupp_upgift_Grupp4.Controllers;
using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
using Grupp_upgift_Grupp4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Grupp_upgift_Grupp4.Models.Entities.User;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace Group_upgift_Grupp4.Test_1.Controller
{
    public class TestAuction : ControllerBase
    {
        private readonly IAuctionServices _auctionServices;
        private readonly IAuctionRepo _auctionRepo;

        public TestAuction()
        {
            _auctionServices = A.Fake<IAuctionServices>();
        }

        [Fact]
        public void AuctionController_UpdateAuction_ReturnsBadRequest()
        {
            //Arrange
            //string username = "1";
            Auctions auctions = A.Fake<Auctions>();
            AuctionController controller = new AuctionController(_auctionServices);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "testuser") })) }
            };

            //Act
            var result = controller.Update(auctions) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
    }
}
