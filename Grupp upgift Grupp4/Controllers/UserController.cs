using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grupp_upgift_Grupp4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }
            
    }
}
