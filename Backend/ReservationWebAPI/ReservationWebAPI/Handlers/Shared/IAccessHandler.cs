using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public interface IAccessHandler
    {
        public Task CheckAccessRightByUserIdAsync(int userId);
        public void CheckAccessRightByEmailAsync(string email);
        public Task CheckAccessRightByReservationIdAsync(int reservationId);
        public Task CheckAccessRightByReservationAsync(Reservation reservation);
    }
}
