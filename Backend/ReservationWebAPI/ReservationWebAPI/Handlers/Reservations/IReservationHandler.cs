using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public interface IReservationHandler
    {
        public Task<Reservation> GetReservationAsync(int reservationId);
        public Task AddReservationAsync(Reservation reservation);
        public Task DeleteReservationAsync(int reservationId);
    }
}
