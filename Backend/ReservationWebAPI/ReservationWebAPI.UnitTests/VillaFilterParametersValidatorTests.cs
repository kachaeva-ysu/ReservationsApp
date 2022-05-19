using Xunit;
using System;

namespace ReservationWebAPI.UnitTests
{
    public class VillaFilterParametersValidatorTests
    {
        private VillaFilterParametersValidator _validator=new VillaFilterParametersValidator();

        [Fact]
        public void ShouldHaveValidationErrorIfStartDateIsEarlierThanToday()
        {
            var parameters = new VillaFilterParameters { StartDate = DateTime.Now.AddDays(-1) };

            var errors = _validator.Validate(parameters).Errors;

            Assert.Single(errors);
            Assert.Equal("StartDate", errors[0].PropertyName);
            Assert.Equal("Invalid Start Date", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldHaveValidationErrorIfEndDateIsEarlierThanToday()
        {
            var parameters = new VillaFilterParameters { EndDate = DateTime.Now.AddDays(-1) };

            var errors = _validator.Validate(parameters).Errors;

            Assert.Single(errors);
            Assert.Equal("EndDate", errors[0].PropertyName);
            Assert.Equal("Invalid End Date", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldHaveValidationErrorIfMinNumberOfRoomsIsLessThanZero()
        {
            var parameters = new VillaFilterParameters { MinNumberOfRooms = -1 };

            var errors = _validator.Validate(parameters).Errors;

            Assert.Single(errors);
            Assert.Equal("MinNumberOfRooms", errors[0].PropertyName);
            Assert.Equal("Invalid Min Number Of Rooms", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldHaveValidationErrorIfMaxNumberOfRoomsIsLessThanZero()
        {
            var parameters = new VillaFilterParameters { MaxNumberOfRooms = -1 };

            var errors = _validator.Validate(parameters).Errors;

            Assert.Single(errors);
            Assert.Equal("MaxNumberOfRooms", errors[0].PropertyName);
            Assert.Equal("Invalid Max Number Of Rooms", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldHaveValidationErrorIfMinPriceForDayIsLessThanZero()
        {
            var parameters = new VillaFilterParameters { MinPriceForDay = -1 };

            var errors = _validator.Validate(parameters).Errors;

            Assert.Single(errors);
            Assert.Equal("MinPriceForDay", errors[0].PropertyName);
            Assert.Equal("Invalid Min Price For Day", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldHaveValidationErrorIfMaxPriceForDayIsLessThanZero()
        {
            var parameters = new VillaFilterParameters { MaxPriceForDay = -1 };

            var errors = _validator.Validate(parameters).Errors;

            Assert.Single(errors);
            Assert.Equal("MaxPriceForDay", errors[0].PropertyName);
            Assert.Equal("Invalid Max Price For Day", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldHaveValidationErrorIfEndDateIsEarlierThanStartDate()
        {
            var parameters = new VillaFilterParameters { StartDate=DateTime.Now.AddDays(1), EndDate=DateTime.Now };

            var errors = _validator.Validate(parameters).Errors;

            Assert.Single(errors);
            Assert.Equal("EndDate", errors[0].PropertyName);
            Assert.Equal("End date cannot be earlier than start date", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldHaveValidationErrorIfMaxPriceForDayIsLessThanMinPriceForDay()
        {
            var parameters = new VillaFilterParameters { MinPriceForDay = 2, MaxPriceForDay = 1 };

            var errors = _validator.Validate(parameters).Errors;

            Assert.Single(errors);
            Assert.Equal("MaxPriceForDay", errors[0].PropertyName);
            Assert.Equal("Max price for day cannot be less than min price for day", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldHaveValidationErrorIfMaxNumberOfRoomsIsLessThanMinNumberOfRooms()
        {
            var parameters = new VillaFilterParameters { MinNumberOfRooms = 2, MaxNumberOfRooms = 1 };

            var errors = _validator.Validate(parameters).Errors;

            Assert.Single(errors);
            Assert.Equal("MaxNumberOfRooms", errors[0].PropertyName);
            Assert.Equal("Max number of rooms cannot be less than min number of rooms", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldNotHaveAnyValidationErrorsIfFilterParametersAreValid()
        {
            var parameters = new VillaFilterParameters { StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), MinNumberOfRooms = 1, MaxNumberOfRooms = 2, MinPriceForDay = 1, MaxPriceForDay = 2, HasPool = true };

            var result = _validator.Validate(parameters);

            Assert.True(result.IsValid);
        }
    }
}