using Azure.Core;
using Grupp_upgift_Grupp4.Repository.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using Grupp_upgift_Grupp4.Models.Entities;
using Grupp_upgift_Grupp4.Repository.Repo;
using Microsoft.AspNetCore.Authorization;
using static System.Data.Entity.Infrastructure.Design.Executor;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Grupp_upgift_Grupp4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo, IHttpContextAccessor httpContextAccessor)
        {
            _userRepo = userRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("show")]
        public IActionResult GetUserInfo()
        {
            try
            {
               var username = User.FindFirst(ClaimTypes.Name)?.Value;
               var result = _userRepo.GetUser(username);

                if (username == null)
                {
                    return NotFound($"Account with Username {username} not found.");
                }

                return Ok(result);
            }

            
            catch (Exception ex)
            {
                // Log the exception details (replace Console.WriteLine with your logging mechanism)
                Console.WriteLine($"An error occurred in GetAll: {ex.Message}");

                // Return HTTP 500 Internal Server Error with an error message
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpPost("addUser")]
        public IActionResult Insert(User user)
        {
            try
            {
                   _userRepo.Insert(user);
                    // If successful, return status code 201
                    return StatusCode(201);
               

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return an appropriate error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
        
        [HttpDelete("Delete")]
        public IActionResult DeleteUser()
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                _userRepo.Delete(username);

                if (username == null)
                {
                    return NotFound($"Account with Username {username} not found.");
                }

                return Ok("User Delete Successfully");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return an appropriate error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(User user)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                _userRepo.Update(user);
                if (user.UserName == username)
                {
                    return Ok("User update Successfully");
                }
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return an appropriate error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
        {
            private readonly IUserRepo _usersRepo;

            //public IUserRepo IUserRepo { get; private set; }

            public BasicAuthenticationHandler(
                IOptionsMonitor<AuthenticationSchemeOptions> options,
                ILoggerFactory logger,
                UrlEncoder encoder,
                ISystemClock clock,
                IUserRepo usersRepo) : base(options, logger, encoder, clock)
            {
                _usersRepo = usersRepo;
            }

            protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                    return AuthenticateResult.Fail("Missing Authorization Header");

                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialsBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(':', 2);
                var username = credentials[0];
                var password = credentials[1];

                var user = await _usersRepo.GetUserByUsernameAndPassword(username, password);

                if (user == null)
                    return AuthenticateResult.Fail("Invalid username or password");

                var claims = new[] {
            new Claim(ClaimTypes.Name, user.UserName)
             //new Claim(ClaimTypes.Role, user.UserType)
        };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }

        }
    }

}
