using Grupp_upgift_Grupp4.Controllers;
using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Interface;
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


namespace Group_upgift_Grupp4.Test_1.Controller
{
    public class UserControllesTest
    {

        [Fact]
        public void Update_ValidUser_ReturnsOk()
        {
            // Arrange
            var mockUserRepo = new Mock<IUserRepo>();

            // Mock the UserRepo to return a user when GetUserByUsernameAndPassword is called
            mockUserRepo.Setup(repo => repo.GetUserByUsernameAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                        .ReturnsAsync((string username, string password) =>
        new User
        (
            userID: 1,
            userName: username,
            password: password,
            firstName: "test",
            lastName: "user"
            ));
            // Create a new instance of UserController with the mockUserRepo
            var userController = new UserController(mockUserRepo.Object);

            // Mock the User property in the controller context
            userController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "testuser") })) }
            };

            // Create a user with matching username
            var userToUpdate = new User(
                userID: 1,
                userName: "testuser",
                password: "ttttt",
                firstName: "test",
                lastName: "user"
                );



            // Act
            var result = userController.Update(userToUpdate) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal("User update Successfully", result.Value);
            mockUserRepo.Verify(repo => repo.Update(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void Update_InvalidUser_ReturnsBadRequest()
        {
            // Arrange
            var mockUserRepo = new Mock<IUserRepo>();

            // Mock the UserRepo to return a user when GetUserByUsernameAndPassword is called
            mockUserRepo.Setup(repo => repo.GetUserByUsernameAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                        .ReturnsAsync((string username, string password) =>
                            new User
                            (
                                userID: 1,
                                userName: username,
                                password: password,
                                firstName: "test",
                                lastName: "user"
                            ));

            // Create a new instance of UserController with the mockUserRepo
            var userController = new UserController(mockUserRepo.Object);

            // Mock the User property in the controller context
            userController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "TestUser123") }))
                }
            };

            // Create a user with matching username
            var userToUpdate = new User(
                userID: 1,
                userName: "testuser222",
                password: "ttttt",
                firstName: "test",
                lastName: "user"
            );

            // Act
            var result = userController.Update(userToUpdate) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            mockUserRepo.Verify(repo => repo.Update(It.IsAny<User>()), Times.Never); // Change to Times.Never
        }
 
    }


}

