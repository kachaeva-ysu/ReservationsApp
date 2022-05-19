using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public interface IReservationRepository
    {
        public Task<IEnumerable<Reservation>> GetReservationsAsync();
        public Task<Reservation> GetReservationAsync(int reservationId);
        public Task AddReservationAsync(Reservation reservation);
        public Task DeleteReservationAsync(Reservation reservation);
    }
}
