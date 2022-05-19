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
    public class UserControllerTests : IClassFixture<ClientFixture>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly IAppRepository _repo;
        private readonly ITokenAuthorizationHandler _authorizationHandler;
        private readonly string _uri = "users";
        private readonly User _testUser = new User { Name = "User", Phone = "123456", Email = "123@123.com", Password="Password" };

        public UserControllerTests(ClientFixture fixture)
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

        //public async ValueTask DisposeAsync()
        //{
        //    var users = await _repo.GetUsersAsync();
        //    foreach (var user in users)
        //        await _repo.DeleteUserAsync(user);
        //}

        [Fact]
        public async Task GetUserReturnsOkAndUserIfTokenIsValid()
        {
            await _repo.AddUserAsync(_testUser);
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{_uri}/{_testUser.Id}");
            var validToken = _authorizationHandler.GetToken(_testUser.Id);
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(validToken));

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var user = response.Content.ReadFromJsonAsync<User>().Result;

            Assert.True(_testUser.ValueEquals(user));
        }

        [Fact]
        public async Task DeleteUserReturnsNoContent()
        {
            await _repo.AddUserAsync(_testUser);
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"{_uri}/{_testUser.Id}");
            var validToken = _authorizationHandler.GetToken(_testUser.Id);
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(validToken));

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task UpdateUserReturnsNoContent()
        {
            await _repo.AddUserAsync(_testUser);
            var userInfoForUpdate = new UserInfoForUpdate { { "name", "User2" } };
            using var request = new HttpRequestMessage(HttpMethod.Patch, $"{_uri}/{_testUser.Id}");
            request.Content = StringContentMaker.GetBody(userInfoForUpdate);
            var validToken = _authorizationHandler.GetToken(_testUser.Id);
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(validToken));

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task GetUserReturnsNotFoundAndMessageIfThereIsNoUserWithRequestedId()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{_uri}/1");
            var validToken = _authorizationHandler.GetToken(1);
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(validToken));

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("No user with this ID", message);
        }

        [Fact]
        public async Task GetUserReturnsUnauthorizedAndMessageIfThereIsNoAuthorizationHeader()
        {
            await _repo.AddUserAsync(_testUser);

            var response = await _client.GetAsync($"{_uri}/{_testUser.Id}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("Authorization required", message);
        }

        [Fact]
        public async Task GetUserReturnsUnauthorizedAndMessageIfTokenObjectIsInvalid()
        {
            await _repo.AddUserAsync(_testUser);
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{_uri}/{_testUser.Id}");
            var token = new { token = "123" };
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(token));

            var response = await _client.GetAsync($"{_uri}/{_testUser.Id}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("Authorization required", message);
        }

        [Fact]
        public async Task GetUserReturnsUnauthorizedAndMessageIfTokenIsInvalid()
        {
            await _repo.AddUserAsync(_testUser);
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{_uri}/{_testUser.Id}");
            var token = _authorizationHandler.GetToken(_testUser.Id);
            token.AccessToken += 'a';
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(token));

            var response = await _client.GetAsync($"{_uri}/{_testUser.Id}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("Authorization required", message);
        }

        [Fact]
        public async Task GetUserReturnsUnauthorizedAndMessageIfTokenIsExpired()
        {
            await _repo.AddUserAsync(_testUser);
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{_uri}/{_testUser.Id}");
            var token = _authorizationHandler.GetToken(_testUser.Id);
            token.ExpirationTime = DateTime.Now;
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(token));

            var response = await _client.GetAsync($"{_uri}/{_testUser.Id}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("Authorization required", message);
        }

        [Fact]
        public async Task DeleteUserReturnsUnauthorizedAndMessageIfTokenIsInvalid()
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"{_uri}/1");
            var token = _authorizationHandler.GetToken(1);
            token.AccessToken += 'a';
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(token));

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("Authorization required", message);
        }

        [Fact]
        public async Task DeleteUserReturnsNotFoundAndMessageIfUserDoesNotExists()
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"{_uri}/1");
            var validToken = _authorizationHandler.GetToken(1);
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(validToken));

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("No user with this ID", message);
        }

        [Fact]
        public async Task UpdateUserReturnsUnauthorizedAndMessageIfTokenIsInvalid()
        {
            var userInfoForUpdate = new UserInfoForUpdate { { "name", "User2" } };
            using var request = new HttpRequestMessage(HttpMethod.Patch, $"{_uri}/1");
            request.Content = StringContentMaker.GetBody(userInfoForUpdate);
            var token = _authorizationHandler.GetToken(1);
            token.AccessToken += 'a';
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(token));

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("Authorization required", message);
        }

        [Fact]
        public async Task UpdateUserReturnsNotFoundAndMessageIfUserDoesNotExists()
        {
            var userInfoForUpdate = new UserInfoForUpdate { { "name", "User2" } };
            using var request = new HttpRequestMessage(HttpMethod.Patch, $"{_uri}/1");
            var validToken = _authorizationHandler.GetToken(1);
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(validToken));
            request.Content = StringContentMaker.GetBody(userInfoForUpdate);

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("No user with this ID", message);
        }

        [Fact]
        public async Task UpdateUserReturnsBadRequestAndDoesNotUpdateUserIfUserInfoForUpdateIsInvalid()
        {
            await _repo.AddUserAsync(_testUser);
            var userInfoForUpdate = new UserInfoForUpdate { { "name", "" } };
            using var request = new HttpRequestMessage(HttpMethod.Patch, $"{_uri}/{_testUser.Id}");
            var validToken = _authorizationHandler.GetToken(_testUser.Id);
            request.Headers.TryAddWithoutValidation("Authorization", JsonConvert.SerializeObject(validToken));
            request.Content = StringContentMaker.GetBody(userInfoForUpdate);

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.True(_testUser.ValueEquals(await _repo.GetUserAsync(_testUser.Id)));
        }
    }
}
