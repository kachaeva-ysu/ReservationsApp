using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ReservationWebAPI.UnitTests
{
    public class UserActionHandlerTests
    {
        IAppRepository _repo;
        UserActionHandler _userActionHandler;
        IPasswordHandler _passwordHandler = new PasswordHandler(Guid.NewGuid().ToString());
        User _testUser = new User { Id = 1, Name = "Name", Email = "Email", Phone = "123456" };

        public UserActionHandlerTests()
        {
            var options = new DbContextOptionsBuilder<AppContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _repo = new AppContext(options);
            _userActionHandler = new UserActionHandler(_repo, _passwordHandler);
        }

        [Fact]
        public async Task GetUserReturnsUserIfUserExists()
        {
            await _repo.AddUserAsync(_testUser);

            Assert.Equal(_testUser, await _userActionHandler.GetUserAsync(1));
        }

        [Fact]
        public async Task DeleteUserWorksCorrectly()
        {
            await _repo.AddUserAsync(_testUser);

            await _userActionHandler.DeleteUserAsync(1);

            Assert.Empty(await _repo.GetUsersAsync());
        }

        [Fact]
        public async Task UpdateUserUpdatesName()
        {
            await _repo.AddUserAsync(_testUser);
            var newName = "Name2";
            var userInfoForUpdate = new UserInfoForUpdate();
            userInfoForUpdate.Add("Name", newName);

            await _userActionHandler.UpdateUserAsync(1, userInfoForUpdate);

            var updatedUser = _testUser;
            updatedUser.Name = newName;

            Assert.Equal(updatedUser, await _repo.GetUserAsync(1));
        }

        [Fact]
        public async Task UpdateUserUpdatesPhone()
        {
            await _repo.AddUserAsync(_testUser);
            var newPhone = "654321";
            var userInfoForUpdate = new UserInfoForUpdate();
            userInfoForUpdate.Add("Phone", newPhone);

            await _userActionHandler.UpdateUserAsync(1, userInfoForUpdate);

            var updatedUser = _testUser;
            updatedUser.Phone = newPhone;

            Assert.Equal(updatedUser, await _repo.GetUserAsync(1));
        }

        [Fact]
        public async Task UpdateUserUpdatesPassword()
        {
            await _repo.AddUserAsync(_testUser);
            var newPassword = "Password2";
            var userInfoForUpdate = new UserInfoForUpdate();
            userInfoForUpdate.Add("Password", newPassword);

            await _userActionHandler.UpdateUserAsync(1, userInfoForUpdate);

            var updatedUser = _testUser;
            updatedUser.Password = newPassword;

            Assert.Equal(updatedUser, await _repo.GetUserAsync(1));
        }

        [Fact]
        public async Task UpdateUserUpdatesAllParameters()
        {
            await _repo.AddUserAsync(_testUser);
            var newName = "Name2";
            var newPhone = "654321";
            var newPassword = "Password2";
            var userInfoForUpdate = new UserInfoForUpdate();
            userInfoForUpdate.Add("Name", newName);
            userInfoForUpdate.Add("Phone",newPhone );
            userInfoForUpdate.Add("Password", newPassword);

            await _userActionHandler.UpdateUserAsync(1, userInfoForUpdate);

            var updatedUser = _testUser;
            updatedUser.Name = newName;
            updatedUser.Phone = newPhone;
            updatedUser.Password = newPassword;

            Assert.Equal(updatedUser, await _repo.GetUserAsync(1));
        }

        [Fact]
        public async Task GetNonExistentUserThrowsNotFoundException()
        {
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => 
                await _userActionHandler.GetUserAsync(1));
            Assert.Equal("No user with this ID", exception.Message);
        }

        [Fact]
        public async Task DeleteNonExistentUserThrowsNotFoundException()
        {
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => 
                await _userActionHandler.DeleteUserAsync(1));
            Assert.Equal("No user with this ID", exception.Message);
        }

        [Fact]
        public async Task UpdateNonExistentUserThrowsNotFoundException()
        {
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => 
                await _userActionHandler.UpdateUserAsync(1, new UserInfoForUpdate()));
            Assert.Equal("No user with this ID", exception.Message);
        }
    }
}
