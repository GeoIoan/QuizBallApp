using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizballApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet(Name = "Auth")]
        [Authorize]
        public IActionResult Auth()
        {
            return Ok();
        }
    }
}
