using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ReservationWebAPI.UnitTests
{
    public  class AuthorizationActionHandlerTests
    {
        IAppRepository _repo;
        IAuthorizationActionHandler _authorizationActionHandler;
        IPasswordHandler _passwordHandler = new PasswordHandler(Guid.NewGuid().ToString());
        ITokenAuthorizationHandler _tokenAuthorizationHandler = new TokenAuthorizationHandler("secretKey");
        User _testUser = new User { Id = 1, Name = "Name", Email = "Email", Phone = "123456" };
        IUserInfoFromToken _userInfoFromToken = new UserInfoFromToken { Name = "Name" };

        public AuthorizationActionHandlerTests()
        {
            var options = new DbContextOptionsBuilder<AppContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _repo = new AppContext(options);
            _authorizationActionHandler = new AuthorizationActionHandler(_repo, _passwordHandler, _tokenAuthorizationHandler, _userInfoFromToken);
        }

        [Fact]
        public async Task SignInWithPasswordReturnsUserIdIfUserExists()
        {
            await _repo.AddUserAsync(_testUser);

            var userAuthorizationInfo = await _authorizationActionHandler.SignInAsync(_testUser.Email, _testUser.Password);

            Assert.Equal(_testUser.Id, userAuthorizationInfo.UserId);
        }

        [Fact]
        public async Task SignInWithoutPasswordReturnsUserIdIfUserExists()
        {
            await _repo.AddUserAsync(_testUser);

            var userAuthorizationInfo = await _authorizationActionHandler.SignInAsync(_testUser.Email);

            Assert.Equal(_testUser.Id, userAuthorizationInfo.UserId);
        }

        [Fact]
        public async Task GetUserIdWithoutPasswordReturnsNewUserIdAndAddsUserIfUserDoesNotExists()
        {
            var userAuthorizationInfo = await _authorizationActionHandler.SignInAsync(_testUser.Email);
            var expectedUser = new User { Id = 1, Email = _testUser.Email, Name = _testUser.Name };

            Assert.Equal(1, userAuthorizationInfo.UserId);
            Assert.Single(await _repo.GetUsersAsync());
            Assert.True(expectedUser.ValueEquals(await _repo.GetUserAsync(1)));
        }

        [Fact]
        public async Task AddUserWorksCorrectly()
        {
            await _authorizationActionHandler.AddUserAsync(_testUser);

            Assert.Single(await _repo.GetUsersAsync());
            Assert.Equal(_testUser, await _repo.GetUserAsync(1));
        }

        [Fact]
        public async Task GetNonExistentUserIdWithPasswordThrowsNotFoundException()
        {
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => 
                _authorizationActionHandler.SignInAsync(_testUser.Email, _testUser.Password));
            Assert.Equal("No user with this email", exception.Message);
        }

        [Fact]
        public async Task GetUserIdWithPasswordThrowsBadRequestExceptionIfPasswordIsInvalid()
        {
            await _repo.AddUserAsync(_testUser);
            var wrongPassword = $"Wrong {_testUser.Password}";

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => 
                _authorizationActionHandler.SignInAsync(_testUser.Email, wrongPassword));
            Assert.Equal("Invalid password", exception.Message);
        }

        [Fact]
        public async Task AddUserThrowsBadRequestEceptionIfEmailIsNotAvailable()
        {
            await _repo.AddUserAsync(_testUser);

            var exception = await Assert.ThrowsAsync<BadRequestException>(() => 
                _authorizationActionHandler.AddUserAsync(_testUser));
            Assert.Equal("User with this email already exists", exception.Message);
        }
    }
}
