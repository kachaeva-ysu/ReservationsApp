using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public class ReservationActionHandler: IReservationActionHandler
    {
        private readonly IAppRepository _repo;

        public ReservationActionHandler(IAppRepository repo)
        {
            _repo = repo;
        }

        public async Task<Reservation> GetReservationAsync(int reservationId)
        {
            var reservation = await _repo.GetReservationAsync(reservationId);

            if (reservation == null)
                throw new NotFoundException("No reservation with this ID");

            return reservation;
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            CheckIfDatesAreAvailable(reservation.VillaId, await _repo.GetReservationsAsync(), new ReservationDates(reservation.StartDate, reservation.EndDate));
            await _repo.AddReservationAsync(reservation);
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            var reservation = await GetReservationAsync(reservationId);
            await _repo.DeleteReservationAsync(reservation);
        }

        private void CheckIfDatesAreAvailable(int villaId, IEnumerable<Reservation> reservations, ReservationDates reservationDates)
        {
            if (!ReservationDatesHandler.CheckIfDatesAreAvailable(villaId, reservations, reservationDates))
                throw new BadRequestException("Dates are not available");
        }
    }
}
