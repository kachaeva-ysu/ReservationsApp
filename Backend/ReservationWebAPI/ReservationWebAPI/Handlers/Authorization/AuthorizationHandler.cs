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

        public UserAuthorizationInfo GetUserAuthorizationInfo(int userId)
        {
            return _authorizationActionHandler.GetUserAuthorizationInfo(userId);
        }

        public async Task<int> GetUserIdAsync(string email, string password)
        {
            return await _authorizationActionHandler.GetUserIdAsync(email, password);
        }

        public async Task<int> GetUserIdAsync(string email)
        {
            _accessHandler.CheckAccessRightByEmail(email);
            return await _authorizationActionHandler.GetUserIdAsync(email);
        }

        public Token UpdateToken(int userId)
        {
            _accessHandler.CheckAccessRightByUserId(userId);
            return _authorizationActionHandler.UpdateToken(userId);
        }

        public async Task<int> AddUserAsync(User user)
        {
            return await _authorizationActionHandler.AddUserAsync(user);
        }
    }
}
