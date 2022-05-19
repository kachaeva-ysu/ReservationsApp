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
            _accessHandler.CheckAccessRightByUserId(userId);
            await _userActionHandler.DeleteUserAsync(userId);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            _accessHandler.CheckAccessRightByUserId(userId);
            return await _userActionHandler.GetUserAsync(userId);
        }

        public async Task<IEnumerable<UsersReservation>> GetUsersReservationsAsync(int userId)
        {
            _accessHandler.CheckAccessRightByUserId(userId);
            return await _userActionHandler.GetUsersReservationsAsync(userId);
        }

        public async Task UpdateUserAsync(int userId, UserInfoForUpdate userInfoForUpdate)
        {
            _accessHandler.CheckAccessRightByUserId(userId);
            await _userActionHandler.UpdateUserAsync(userId, userInfoForUpdate);
        }
    }
}
