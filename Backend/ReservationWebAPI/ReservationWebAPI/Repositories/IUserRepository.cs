using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User> GetUserAsync(int userId);
        public Task<User> GetUserAsync(string email);
        public Task<IEnumerable<UsersReservation>> GetUsersReservationsAsync(int userId);
        public Task AddUserAsync(User user);
        public Task DeleteUserAsync(User user);
        public Task UpdateUserAsync(User user);
    }
}
