using Xunit;

namespace ReservationWebAPI.UnitTests
{
    public class VillaValidatorTests
    {
        VillaValidator _validator = new VillaValidator();

        [Fact]
        public void ShouldHaveValidationErrorIfNameWasNotPassed()
        {
            var villa = new Villa { PriceForDay = 2, NumberOfRooms = 2};

            var errors = _validator.Validate(villa).Errors;

            Assert.Single(errors);
            Assert.Equal("Name", errors[0].PropertyName);
            Assert.Equal("Name cannot be empty", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldHaveValidationErrorIfNameIsEmpty()
        {
            var villa = new Villa { Name = "", PriceForDay = 2, NumberOfRooms = 2 };

            var errors = _validator.Validate(villa).Errors;

            Assert.Single(errors);
            Assert.Equal("Name", errors[0].PropertyName);
            Assert.Equal("Name cannot be empty", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldHaveValidationErrorIfPriceForDayWasNotPassed()
        {
            var villa = new Villa { Name = "Villa1", NumberOfRooms = 2 };

            var errors = _validator.Validate(villa).Errors;

            Assert.Single(errors);
            Assert.Equal("PriceForDay", errors[0].PropertyName);
            Assert.Equal("Price For Day cannot be empty", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldHaveValidationErrorIfPriceForDayIsZero()
        {
            var villa = new Villa { Name = "Villa1", PriceForDay = 0, NumberOfRooms = 2 };

            var errors = _validator.Validate(villa).Errors;

            Assert.Single(errors);
            Assert.Equal("PriceForDay", errors[0].PropertyName);
            Assert.Equal("Price For Day cannot be empty", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldHaveValidationErrorIfNumberOfRoomsWasNotPassed()
        {
            var villa = new Villa { Name = "Villa1", PriceForDay = 2 };

            var errors = _validator.Validate(villa).Errors;

            Assert.Single(errors);
            Assert.Equal("NumberOfRooms", errors[0].PropertyName);
            Assert.Equal("Number Of Rooms cannot be empty", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldHaveValidationErrorIfNumberOfRoomsIsZero()
        {
            var villa = new Villa { Name = "Villa1", PriceForDay = 2, NumberOfRooms = 0 };

            var errors = _validator.Validate(villa).Errors;

            Assert.Single(errors);
            Assert.Equal("NumberOfRooms", errors[0].PropertyName);
            Assert.Equal("Number Of Rooms cannot be empty", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldNotHaveAnyValidationErrorsIfVillaIsValid()
        {
            var villa = new Villa { Name = "Villa1", Description = "Descr1", NumberOfRooms = 2, PriceForDay = 2, HasPool = false };
            
            var result = _validator.Validate(villa);

            Assert.True(result.IsValid);
        }
    }
}
