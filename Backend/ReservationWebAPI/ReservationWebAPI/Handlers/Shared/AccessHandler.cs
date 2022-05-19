using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public class AccessHandler: IAccessHandler
    {
        private IUserInfoFromToken _userInfoFromToken;
        private IReservationActionHandler _reservationActionHandler;

        public AccessHandler(IUserInfoFromToken userIdFromToken, IReservationActionHandler reservationActionHandler)
        {
            _userInfoFromToken = userIdFromToken;
            _reservationActionHandler = reservationActionHandler;
        }

        public void CheckAccessRightByUserId(int userId)
        {
            if (userId != _userInfoFromToken.UserId)
                throw new ForbiddenException("Access denied");
        }

        public void CheckAccessRightByEmail(string email)
        {
            if(email != _userInfoFromToken.Email)
                throw new ForbiddenException("Access denied");
        }

        public async Task CheckAccessRightByReservationIdAsync(int reservationId)
        {
            var reservation = await _reservationActionHandler.GetReservationAsync(reservationId);
            var userId = reservation.UserId;
            CheckAccessRightByUserId(userId);
        }

        public void CheckAccessRightByReservation(Reservation reservation)
        {
            var userId = reservation.UserId;
            CheckAccessRightByUserId(userId);
        }
    }
}
