using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public interface IAccessHandler
    {
        public void CheckAccessRightByUserId(int userId);
        public void CheckAccessRightByEmail(string email);
        public Task CheckAccessRightByReservationIdAsync(int reservationId);
        public void CheckAccessRightByReservation(Reservation reservation);
    }
}
