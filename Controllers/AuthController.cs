using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizballApp.Controllers
{
    /// <summary>
    /// This class contains only one method that is
    /// used for authorization.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// This method is used for Authorization. When 
        /// a user tries to visit a static page on the clients
        /// side and the page demands that the this user
        /// is authenticated, this method is called to check
        /// the credentials of the user.
        /// </summary>
        /// <returns>(IActionResult) the repsonse to the request</returns>
        [HttpGet("auth")]
        [Authorize]
        public IActionResult Auth()
        {
            return Ok();
        }
    }
}
