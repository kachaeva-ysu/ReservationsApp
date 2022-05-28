using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public class AuthorizationHandler : IAuthorizationHandler
    {
        private readonly IAuthorizationActionHandler _authorizationActionHandler;
        private readonly IAccessHandler _accessHandler;

        public AuthorizationHandler(IAuthorizationActionHandler authorizationActionHandler, IAccessHandler accessHandler)
        {
            _authorizationActionHandler = authorizationActionHandler;
            _accessHandler = accessHandler;
        }

        public async Task<UserAuthorizationInfo> SignInAsync(string email, string password)
        {
            return await _authorizationActionHandler.SignInAsync(email, password);
        }

        public async Task<UserAuthorizationInfo> SignInAsync(string email)
        {
            await _accessHandler.CheckAccessRightByEmailAsync(email);
            return await _authorizationActionHandler.SignInAsync(email);
        }

        public async Task<Token> UpdateTokenAsync(int userId)
        {
            await _accessHandler.CheckAccessRightByUserIdAsync(userId);
            return await _authorizationActionHandler.UpdateTokenAsync(userId);
        }

        public async Task<UserAuthorizationInfo> AddUserAsync(User user)
        {
            return await _authorizationActionHandler.AddUserAsync(user);
        }
    }
}
