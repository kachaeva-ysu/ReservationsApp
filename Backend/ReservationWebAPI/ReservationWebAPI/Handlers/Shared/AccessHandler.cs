using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public class AccessHandler: IAccessHandler
    {
        private IUserInfoFromToken _userInfoFromToken;
        private IReservationActionHandler _reservationActionHandler;
        private IUserActionHandler _userActionHandler;

        public AccessHandler(IUserInfoFromToken userIdFromToken, IReservationActionHandler reservationActionHandler, IUserActionHandler userActionHandler)
        {
            _userInfoFromToken = userIdFromToken;
            _reservationActionHandler = reservationActionHandler;
            _userActionHandler = userActionHandler;
        }

        public async Task CheckAccessRightByUserIdAsync(int userId)
        {
            await CheckRefreshTokenAsync(userId);
            if (userId != _userInfoFromToken.UserId)
                throw new ForbiddenException("Access denied");
        }

        public void CheckAccessRightByEmailAsync(string email)
        {
            if(email != _userInfoFromToken.Email)
                throw new ForbiddenException("Access denied");
        }

        public async Task CheckAccessRightByReservationIdAsync(int reservationId)
        {
            var reservation = await _reservationActionHandler.GetReservationAsync(reservationId);
            var userId = reservation.UserId;
            await CheckAccessRightByUserIdAsync(userId);
        }

        public async Task CheckAccessRightByReservationAsync(Reservation reservation)
        {
            var userId = reservation.UserId;
            await CheckAccessRightByUserIdAsync(userId);
        }

        private async Task CheckRefreshTokenAsync(int userId)
        {
            var user = await _userActionHandler.GetUserAsync(userId);
            if(user.RefreshToken!=_userInfoFromToken.RefreshToken)
                throw new ForbiddenException("Access denied");
        }
    }
}
