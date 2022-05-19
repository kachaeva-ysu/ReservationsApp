using System;
using Xunit;

namespace ReservationWebAPI.UnitTests
{
    public class AuthorizationHandlerTests
    {
        ITokenAuthorizationHandler _authorizationHandler = new TokenAuthorizationHandler(Guid.NewGuid().ToString());
        Token _testToken;

        public AuthorizationHandlerTests()
        {
            _testToken = _authorizationHandler.GetToken(1);
        }

        [Fact]
        public void CheckIfTokenIsValidAndUnexpiredReturnsTrueIfTokenIsCorrect()
        {
            var res = _authorizationHandler.CheckIfTokenIsValidAndUnexpired(_testToken);

            Assert.True(res);
        }

        [Fact]
        public void CheckIfTokenIsValidAndUnexpiredReturnsFalseIfTokenIsExpired()
        {
            var expiredToken = _testToken;
            expiredToken.ExpirationTime = DateTime.Now;

            var res = _authorizationHandler.CheckIfTokenIsValidAndUnexpired(expiredToken);

            Assert.False(res);
        }

        [Fact]
        public void CheckIfTokenIsValidAndUnexpiredReturnsFalseIfTokenIsInvalid()
        {
            var invalidToken = _testToken;
            invalidToken.AccessToken = invalidToken.AccessToken + "a";

            var res = _authorizationHandler.CheckIfTokenIsValidAndUnexpired(invalidToken);

            Assert.False(res);
        }
    }
}
