using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserActionHandler _userActionHandler;
        private readonly IAccessHandler _accessHandler;

        public UserHandler(IUserActionHandler userActionHandler, IAccessHandler accessHandler)
        {
            _userActionHandler = userActionHandler;
            _accessHandler = accessHandler;
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _accessHandler.CheckAccessRightByUserIdAsync(userId);
            await _userActionHandler.DeleteUserAsync(userId);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            await _accessHandler.CheckAccessRightByUserIdAsync(userId);
            return await _userActionHandler.GetUserAsync(userId);
        }

        public async Task<User> GetUserAsync(string email)
        {
            await _accessHandler.CheckAccessRightByEmailAsync(email);
            return await _userActionHandler.GetUserAsync(email);
        }

        public async Task<IEnumerable<UsersReservation>> GetUsersReservationsAsync(int userId)
        {
            await _accessHandler.CheckAccessRightByUserIdAsync(userId);
            return await _userActionHandler.GetUsersReservationsAsync(userId);
        }

        public async Task UpdateUserAsync(int userId, UserInfoForUpdate userInfoForUpdate)
        {
            await _accessHandler.CheckAccessRightByUserIdAsync(userId);
            await _userActionHandler.UpdateUserAsync(userId, userInfoForUpdate);
        }
    }
}
