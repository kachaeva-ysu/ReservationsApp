using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public class UserActionHandler : IUserActionHandler
    {
        private readonly IAppRepository _repo;
        private readonly IPasswordHandler _passwordHandler;

        public UserActionHandler(IAppRepository repo, IPasswordHandler passwordHandler)
        {
            _repo = repo;
            _passwordHandler = passwordHandler;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            var user = await _repo.GetUserAsync(userId);

            if (user == null)
                throw new NotFoundException("No user with this ID");

            return user;
        }

        public async Task<User> GetUserAsync(string email)
        {
            var user = await _repo.GetUserAsync(email);

            if (user == null)
                throw new NotFoundException("No user with this email");

            return user;
        }

        public async Task<IEnumerable<UsersReservation>> GetUsersReservationsAsync(int userId)
        {
            await GetUserAsync(userId);
            return await _repo.GetUsersReservationsAsync(userId);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await GetUserAsync(userId);
            await _repo.DeleteUserAsync(user);
        }

        public async Task UpdateUserAsync(int userId, UserInfoForUpdate userInfoForUpdate)
        {
            var user = await GetUserAsync(userId);

            if (userInfoForUpdate.ContainsKey("name"))
                user.Name = userInfoForUpdate["name"];
            if (userInfoForUpdate.ContainsKey("phone"))
                user.Phone = userInfoForUpdate["phone"];
            if (userInfoForUpdate.ContainsKey("password"))
            {
                VerifyOldPassword(userInfoForUpdate, user.Email, user.Password);
                user.Password = _passwordHandler.GetHashedPassword(userInfoForUpdate["password"], user.Email);
            }

            await _repo.UpdateUserAsync(user);
        }

        private void VerifyOldPassword(UserInfoForUpdate userInfoForUpdate, string email, string password)
        {
            if (!userInfoForUpdate.ContainsKey("oldPassword") || 
                    _passwordHandler.GetHashedPassword(userInfoForUpdate["oldPassword"], email) != password)
                throw new BadRequestException("Invalid old password");
        }
    }
}