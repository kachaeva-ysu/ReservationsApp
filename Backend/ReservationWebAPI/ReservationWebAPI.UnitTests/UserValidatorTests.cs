using Xunit;

namespace ReservationWebAPI.UnitTests
{
    public class UserValidatorTests
    {
        UserValidator _validator = new UserValidator();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldHaveValidationErrorIfNameWasNotPassed(string name)
        {
            var user = new User { Id = 1, Name = name, Phone = "123456", Email = "123@123.123", Password="12345678" };

            var errors = _validator.Validate(user).Errors;

            Assert.Single(errors);
            Assert.Equal("Name", errors[0].PropertyName);
            Assert.Equal("Invalid Name", errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("aaaaaa")]
        [InlineData("123")]
        [InlineData("12345678901234567890")]
        [InlineData("++123456")]
        public void ShouldHaveValidationErrorIfPhoneIsInvalid(string phone)
        {
            var user = new User { Id = 1, Name = "123456", Phone=phone, Email = "123@123.123", Password = "12345678" };

            var errors = _validator.Validate(user).Errors;

            Assert.Single(errors);
            Assert.Equal("Phone", errors[0].PropertyName);
            Assert.Equal("Invalid Phone", errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("aa.aa")]
        [InlineData("aa@aa")]
        [InlineData("aa@aa.")]
        [InlineData("@aa.aa")]
        public void ShouldHaveValidationErrorIfEmailIsInvalid(string email)
        {
            var user = new User { Id = 1, Name = "123456", Phone = "123456", Email = email, Password = "12345678" };

            var errors = _validator.Validate(user).Errors;

            Assert.Single(errors);
            Assert.Equal("Email", errors[0].PropertyName);
            Assert.Equal("Invalid Email", errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("1234")]
        public void ShouldHaveValidationErrorIfPasswordIsInvalid(string password)
        {
            var user = new User { Id = 1, Name = "123456", Phone = "123456", Email = "123@123.123", Password = password };

            var errors = _validator.Validate(user).Errors;

            Assert.Single(errors);
            Assert.Equal("Password", errors[0].PropertyName);
            Assert.Equal("Invalid Password", errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData("123456")]
        [InlineData("+123456")]
        [InlineData("123456789012345")]
        [InlineData("+123456789012345")]
        public void ShouldNotHaveValidationErrorIfPhoneIsValid(string phone)
        {
            var user = new User { Id = 1, Name = "123456", Phone = phone, Email = "123@123.123", Password = "12345678" };

            var errors = _validator.Validate(user).Errors;

            Assert.Empty(errors);
        }

        [Theory]
        [InlineData("123@123.123")]
        [InlineData("a@b.c")]
        public void ShouldNotHaveValidationErrorIfEmailIsValid(string email)
        {
            var user = new User { Id = 1, Name = "123456", Phone = "123456", Email = email, Password = "12345678" };

            var errors = _validator.Validate(user).Errors;

            Assert.Empty(errors);
        }
    }
}
