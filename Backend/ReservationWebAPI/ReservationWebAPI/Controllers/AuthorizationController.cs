using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    [ApiController]
    [Route("authorization")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationHandler _authorizationHandler;

        public AuthorizationController(IAuthorizationHandler authorizationHandler)
        {
            _authorizationHandler = authorizationHandler;
        }

        [HttpGet("signIn")]
        public async Task<ActionResult<UserAuthorizationInfo>> SignInAsync([FromQuery] string email, string password)
        {
            var userId = await _authorizationHandler.GetUserIdAsync(email, password);
            return Ok(_authorizationHandler.GetUserAuthorizationInfo(userId));
        }

        [GoogleAuthorizationFilter]
        [HttpGet("signInWithGoogle")]
        public async Task<ActionResult<UserAuthorizationInfo>> SignInWithGoogleAsync([FromQuery] string email)
        {
            var userId = await _authorizationHandler.GetUserIdAsync(email);
            return Ok(_authorizationHandler.GetUserAuthorizationInfo(userId));
        }

        [HttpPost("signUp")]
        public async Task<ActionResult<UserAuthorizationInfo>> SignUpAsync([FromBody] User newUser)
        {
            var userId = await _authorizationHandler.AddUserAsync(newUser);
            return CreatedAtAction("GetUserAsync", "User", new { userId }, _authorizationHandler.GetUserAuthorizationInfo(userId));
        }

        [TokenAuthorizationFilter(true)]
        [HttpGet("updateToken")]
        public ActionResult<Token> UpdateToken([FromQuery] int userId)
        {
            return Ok(_authorizationHandler.UpdateToken(userId));
        }
    }
}
