using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public interface IUserHandler
    {
        public Task<User> GetUserAsync(int userId);
        public Task<User> GetUserAsync(string email);
        public Task<IEnumerable<UsersReservation>> GetUsersReservationsAsync(int userId);
        public Task DeleteUserAsync(int userId);
        public Task UpdateUserAsync(int userId, UserInfoForUpdate userInfoForUpdate);
    }
}
