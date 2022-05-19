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

        public async Task<int> GetUserIdAsync(string email, string password)
        {
            var users = await _repo.GetUsersAsync();
            foreach (var user in users)
            {
                if (user.Email == email)
                {
                    if (user.Password == _passwordHandler.GetHashedPassword(password, email))
                    {
                        return user.Id;
                    }
                    throw new BadRequestException("Invalid password");
                }
            }
            throw new NotFoundException("No user with this email");
        }

        public async Task<int> GetUserIdAsync(string email)
        {
            var users = await _repo.GetUsersAsync();
            foreach (var user in users)
            {
                if (user.Email == email)
                    return user.Id;
            }

            var newUser = new User { Email = email, Name = _userInfoFromToken.Name };
            return await AddUserAsync(newUser);
        }

        public UserAuthorizationInfo GetUserAuthorizationInfo(int userId)
        {
            return _authorizationHandler.GetUserAuthorizationInfo(userId);
        }

        public Token UpdateToken(int userId)
        {
            return _authorizationHandler.GetToken(userId);
        }

        public async Task<int> AddUserAsync(User user)
        {
            await CheckIfEmailIsAvailableAsync(user.Email);
            user.Password = _passwordHandler.GetHashedPassword(user.Password, user.Email);
            await _repo.AddUserAsync(user);
            return user.Id;
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
