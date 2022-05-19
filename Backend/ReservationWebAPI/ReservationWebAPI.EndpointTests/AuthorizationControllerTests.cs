using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace ReservationWebAPI.EndpointTests
{
    [Collection("Client collection")]
    public class AuthorizationControllerTests : IClassFixture<ClientFixture>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly IAppRepository _repo;
        private readonly ITokenAuthorizationHandler _authorizationHandler;
        private readonly string _uri = "authorization";
        private readonly User _testUser = new User { Name = "User", Phone = "123456", Email = "123@123.com", Password = "Password" };

        public AuthorizationControllerTests(ClientFixture fixture)
        {
            _client = fixture.Client;
            _repo = fixture.AppRepository;
            _authorizationHandler = fixture.AuthorizationHandler;
        }

        public void Dispose()
        {
            var users = _repo.GetUsersAsync().Result;
            foreach (var user in users)
                _repo.DeleteUserAsync(user).Wait();
        }

        [Fact]
        public async Task SignInReturnsOkAndUserAuthorizationInfoIfUserExists()
        {
            var postResponse = await _client.PostAsync($"{_uri}/signUp", StringContentMaker.GetBody(_testUser));
            _testUser.Id = postResponse.Content.ReadFromJsonAsync<UserAuthorizationInfo>().Result.UserId;

            var response = await _client.GetAsync($"{_uri}/signIn?email={_testUser.Email}&password={_testUser.Password}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var userAuthorizationInfo = response.Content.ReadFromJsonAsync<UserAuthorizationInfo>().Result;

            Assert.Equal(_testUser.Id, userAuthorizationInfo.UserId);
            Assert.True(_authorizationHandler.CheckIfTokenIsValidAndUnexpired(userAuthorizationInfo.Token));
        }

        [Fact]
        public async Task UpdateTokenReturnsOkAndNewTokenIfTokenInHeaderIsValidAndExpired()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{_uri}/updateToken?userId=1");
            var token = _authorizationHandler.GetToken(1);
            token.ExpirationTime = DateTime.Now;
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(token));

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var newToken = response.Content.ReadFromJsonAsync<Token>().Result;

            Assert.True(_authorizationHandler.CheckIfTokenIsValidAndUnexpired(newToken));
        }

        [Fact]
        public async Task AddUserReturnsCreatedAndUserAuthorizationInfo()
        {
            var response = await _client.PostAsync($"{_uri}/signUp", StringContentMaker.GetBody(_testUser));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var userAuthorizationInfo = response.Content.ReadFromJsonAsync<UserAuthorizationInfo>().Result;

            Assert.True(_authorizationHandler.CheckIfTokenIsValidAndUnexpired(userAuthorizationInfo.Token));
        }

        [Fact]
        public async Task SignInReturnsNotFoundAndMessageIfUserDoesNotExist()
        {
            var response = await _client.GetAsync($"{_uri}/signIn?email={_testUser.Email}&password={_testUser.Password}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("No user with this email", message);
        }

        [Fact]
        public async Task SignInReturnsBadRequestAndMessageIfPasswordIsInvalid()
        {
            await _repo.AddUserAsync(_testUser);
            var response = await _client.GetAsync($"{_uri}/signIn?email={_testUser.Email}&password={_testUser.Password}a");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("Invalid password", message);
        }

        [Fact]
        public async Task UpdateTokenReturnsUnauthorizedAndMessageIfTokenIsInvalid()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{_uri}/updateToken?userId=1");
            var token = _authorizationHandler.GetToken(1);
            token.AccessToken += 'a';
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(token));

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("Authorization required", message);
        }

        [Fact]
        public async Task AddUserReturnsBadRequestIfUserWithSameEmailExistsAndDoesNotAddUser()
        {
            await _repo.AddUserAsync(_testUser);

            var response = await _client.PostAsync($"{_uri}/signUp", StringContentMaker.GetBody(_testUser));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("User with this email already exists", message);
            Assert.Single(await _repo.GetUsersAsync());
        }

        [Fact]
        public async Task AddUserReturnsBadRequestAndDoesNotAddUserIfUserIsInvalid()
        {
            var invalidUser = new User { Password = "1234" };

            var response = await _client.PostAsync($"{_uri}/signUp", StringContentMaker.GetBody(invalidUser));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Empty(await _repo.GetUsersAsync());
        }
    }
}
