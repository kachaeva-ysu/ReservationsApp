using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public interface IAuthorizationHandler
    {
        public Task<UserAuthorizationInfo> SignInAsync(string email, string password);
        public Task<UserAuthorizationInfo> SignInAsync(string email);
        public Task<Token> UpdateTokenAsync(int userId);
        public Task<UserAuthorizationInfo> AddUserAsync(User user);

    }
}
