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
            return Ok(await _authorizationHandler.SignInAsync(email, password));
        }

        [GoogleAuthorizationFilter]
        [HttpGet("signInWithGoogle")]
        public async Task<ActionResult<UserAuthorizationInfo>> SignInWithGoogleAsync([FromQuery] string email)
        {
            return Ok(await _authorizationHandler.SignInAsync(email));
        }

        [HttpPost("signUp")]
        public async Task<ActionResult<UserAuthorizationInfo>> SignUpAsync([FromBody] User newUser)
        {
            var userAuthorizationInfo = await _authorizationHandler.AddUserAsync(newUser);
            return CreatedAtAction("GetUserAsync", "User", new { userId = userAuthorizationInfo.UserId}, userAuthorizationInfo);
        }

        [TokenAuthorizationFilter(true)]
        [HttpGet("updateToken")]
        public async Task<ActionResult<Token>> UpdateToken([FromQuery] int userId)
        {
            return Ok(await _authorizationHandler.UpdateTokenAsync(userId));
        }
    }
}
