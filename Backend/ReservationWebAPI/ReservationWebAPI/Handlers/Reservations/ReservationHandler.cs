using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public class ReservationHandler : IReservationHandler
    {
        private readonly IReservationActionHandler _reservationActionHandler;
        private readonly IAccessHandler _accessHandler;

        public ReservationHandler(IReservationActionHandler reservationActionHandler, IAccessHandler accessHandler)
        {
            _reservationActionHandler = reservationActionHandler;
            _accessHandler = accessHandler;
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            await _accessHandler.CheckAccessRightByReservationAsync(reservation);
            await _reservationActionHandler.AddReservationAsync(reservation);
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            await _accessHandler.CheckAccessRightByReservationIdAsync(reservationId);
            await _reservationActionHandler.DeleteReservationAsync(reservationId);
        }

        public async Task<Reservation> GetReservationAsync(int reservationId)
        {
            await _accessHandler.CheckAccessRightByReservationIdAsync(reservationId);
            return await _reservationActionHandler.GetReservationAsync(reservationId);
        }
    }
}
