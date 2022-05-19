using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserHandler _userHandler;

        public UserController(IUserHandler userHandler)
        {
            _userHandler = userHandler;
        }

        [TokenAuthorizationFilter]
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserAsync(int userId)
        {
            return Ok(await _userHandler.GetUserAsync(userId));
        }

        [TokenAuthorizationFilter]
        [HttpGet("{userId}/reservations")]
        public async Task<ActionResult<UsersReservation>> GetUsersReservationsAsync(int userId)
        {
            return Ok(await _userHandler.GetUsersReservationsAsync(userId));
        }

        [TokenAuthorizationFilter]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            await _userHandler.DeleteUserAsync(userId);
            return NoContent();
        }

        [TokenAuthorizationFilter]
        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(int userId, [FromBody] UserInfoForUpdate userInfoForUpdate)
        {
            await _userHandler.UpdateUserAsync(userId, userInfoForUpdate);
            return NoContent();
        }
    }
}
