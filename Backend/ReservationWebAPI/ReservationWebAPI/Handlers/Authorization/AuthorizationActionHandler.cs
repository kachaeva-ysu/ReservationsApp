using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public class AuthorizationActionHandler: IAuthorizationActionHandler
    {
        private readonly IAppRepository _repo;
        private readonly IPasswordHandler _passwordHandler;
        private readonly ITokenAuthorizationHandler _authorizationHandler;
        private readonly IUserInfoFromToken _userInfoFromToken;

        public AuthorizationActionHandler(IAppRepository repo, IPasswordHandler passwordHandler,
            ITokenAuthorizationHandler authorizationHandler, IUserInfoFromToken userInfoFromToken)
        {
            _repo = repo;
            _passwordHandler = passwordHandler;
            _authorizationHandler = authorizationHandler;
            _userInfoFromToken = userInfoFromToken;
        }

        public async Task<UserAuthorizationInfo> SignInAsync(string email, string password)
        {
            var users = await _repo.GetUsersAsync();
            foreach (var user in users)
            {
                if (user.Email == email)
                {
                    if (user.Password == _passwordHandler.GetHashedPassword(password, email))
                        return await GetAndSaveUserAuthorizationInfoAsync(user);
                    throw new BadRequestException("Invalid password");
                }
            }
            throw new NotFoundException("No user with this email");
        }

        public async Task<UserAuthorizationInfo> SignInAsync(string email)
        {
            var users = await _repo.GetUsersAsync();
            foreach (var user in users)
            {
                if (user.Email == email)
                    return await GetAndSaveUserAuthorizationInfoAsync(user);
            }

            var newUser = new User { Email = email, Name = _userInfoFromToken.Name };
            return await AddUserAsync(newUser);
        }

        public async Task<Token> UpdateTokenAsync(int userId)
        {
            return _authorizationHandler.GetToken(userId);
        }

        public async Task<UserAuthorizationInfo> AddUserAsync(User user)
        {
            await CheckIfEmailIsAvailableAsync(user.Email);
            user.Password = _passwordHandler.GetHashedPassword(user.Password, user.Email);
            await _repo.AddUserAsync(user);
            return await GetAndSaveUserAuthorizationInfoAsync(user);
        }

        private async Task<UserAuthorizationInfo> GetAndSaveUserAuthorizationInfoAsync(User user)
        {
            var userAuthorizationInfo = _authorizationHandler.GetUserAuthorizationInfo(user.Id);
            user.RefreshToken = userAuthorizationInfo.Token.RefreshToken;
            await _repo.UpdateUserAsync(user);
            return userAuthorizationInfo;
        }
        private async Task CheckIfEmailIsAvailableAsync(string email)
        {
            var users = await _repo.GetUsersAsync();
            foreach (var user in users)
            {
                if (user.Email == email)
                    throw new BadRequestException("User with this email already exists");
            }
        }
    }
}
