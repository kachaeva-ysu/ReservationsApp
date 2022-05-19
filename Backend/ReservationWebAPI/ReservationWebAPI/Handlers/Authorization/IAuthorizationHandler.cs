using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public interface IAuthorizationHandler
    {
        public Task<int> GetUserIdAsync(string email, string password);
        public Task<int> GetUserIdAsync(string email);
        public UserAuthorizationInfo GetUserAuthorizationInfo(int userId);
        public Token UpdateToken(int userId);
        public Task<int> AddUserAsync(User user);

    }
}
