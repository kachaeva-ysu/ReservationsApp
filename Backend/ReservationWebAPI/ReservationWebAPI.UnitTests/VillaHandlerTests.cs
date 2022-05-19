using Xunit;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ReservationWebAPI.UnitTests
{
    public class VillaHandlerTests
    {
        IAppRepository _repo;
        VillaHandler _villaHandler;
        VillaFilterParameters _filterParameters;
        DateTime _todayDate = DateTime.Now;
        Villa _villa1 = new Villa { Id = 1, Description = "Descr1", HasPool = true, Name = "Villa1", NumberOfRooms = 2, PriceForDay = 200 };
        Villa _villa2 = new Villa { Id = 2, Description = "Descr2", HasPool = false, Name = "Villa2", NumberOfRooms = 5, PriceForDay = 500 };
        Villa _villa3 = new Villa { Id = 3, Description = "Descr3", HasPool = false, Name = "Villa3", NumberOfRooms = 4, PriceForDay = 350 };

        public VillaHandlerTests()
        {
            var options = new DbContextOptionsBuilder<AppContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _repo = new AppContext(options);
            _villaHandler = new VillaHandler(_repo);
            _filterParameters = new VillaFilterParameters();
        }

        [Fact]
        public async Task GetVillasReturnsEmptyVillasWithNoVillas()
        {
            var villas = await _villaHandler.GetVillasAsync(_filterParameters);

            Assert.Empty(villas);
        }

        [Fact]
        public async Task GetVillasReturnsCorrectVillasWithEmptyParameters()
        {
            await _repo.AddVillaAsync(_villa1);
            await _repo.AddVillaAsync(_villa2);

            var villas = (await _villaHandler.GetVillasAsync(_filterParameters)).ToList();

            Assert.Equal(2, villas.Count);
            Assert.True(_villa1.ValueEquals(villas[0]));
            Assert.True(_villa2.ValueEquals(villas[1]));
        }

        [Fact]
        public async Task GetVillasReturnsCorrectVillasWithPoolParameter()
        {
            await _repo.AddVillaAsync(_villa1);
            await _repo.AddVillaAsync(_villa2);
            _filterParameters = new VillaFilterParameters { HasPool = true };

            var villas = (await _villaHandler.GetVillasAsync(_filterParameters)).ToList();

            Assert.Single(villas);
            Assert.True(_villa1.ValueEquals(villas[0]));
        }

        [Fact]
        public async Task GetVillasReturnsCorrectVillasWithMinPriceParameter()
        {
            await _repo.AddVillaAsync(_villa1);
            await _repo.AddVillaAsync(_villa2);
            _filterParameters = new VillaFilterParameters { MinPriceForDay = 300 };

            var villas = (await _villaHandler.GetVillasAsync(_filterParameters)).ToList();

            Assert.Single(villas);
            Assert.True(_villa2.ValueEquals(villas[0]));
        }

        [Fact]
        public async Task GetVillasReturnsCorrectVillasWithMaxPriceParameter()
        {
            await _repo.AddVillaAsync(_villa1);
            await _repo.AddVillaAsync(_villa2);
            _filterParameters = new VillaFilterParameters { MaxPriceForDay = 300 };

            var villas = (await _villaHandler.GetVillasAsync(_filterParameters)).ToList();

            Assert.Single(villas);
            Assert.True(_villa1.ValueEquals(villas[0]));
        }

        [Fact]
        public async Task GetVillasReturnsCorrectVillasWithMinAndMaxPriceParameters()
        {
            await _repo.AddVillaAsync(_villa1);
            await _repo.AddVillaAsync(_villa2);
            await _repo.AddVillaAsync(_villa3);
            _filterParameters = new VillaFilterParameters { MinPriceForDay = 300, MaxPriceForDay = 400 };

            var villas = (await _villaHandler.GetVillasAsync(_filterParameters)).ToList();

            Assert.Single(villas);
            Assert.True(_villa3.ValueEquals(villas[0]));
        }

        [Fact]
        public async Task GetVillasReturnsCorrectVillasWithMinNumberOfRoomsParameter()
        {
            await _repo.AddVillaAsync(_villa1);
            await _repo.AddVillaAsync(_villa2);
            _filterParameters = new VillaFilterParameters { MinNumberOfRooms = 3 };

            var villas = (await _villaHandler.GetVillasAsync(_filterParameters)).ToList();

            Assert.Single(villas);
            Assert.True(_villa2.ValueEquals(villas[0]));
        }

        [Fact]
        public async Task GetVillasReturnsCorrectVillasWithMaxNumberOfRoomsParameter()
        {
            await _repo.AddVillaAsync(_villa1);
            await _repo.AddVillaAsync(_villa2);
            _filterParameters = new VillaFilterParameters { MaxNumberOfRooms = 3 };

            var villas = (await _villaHandler.GetVillasAsync(_filterParameters)).ToList();

            Assert.Single(villas);
            Assert.True(_villa1.ValueEquals(villas[0]));
        }

        [Fact]
        public async Task GetVillasReturnsCorrectVillasWithMinAndMaxNumberOfRoomsParameters()
        {
            await _repo.AddVillaAsync(_villa1);
            await _repo.AddVillaAsync(_villa2);
            await _repo.AddVillaAsync(_villa3);
            _filterParameters = new VillaFilterParameters { MinNumberOfRooms = 3, MaxNumberOfRooms = 4 };

            var villas = (await _villaHandler.GetVillasAsync(_filterParameters)).ToList();

            Assert.Single(villas);
            Assert.True(_villa3.ValueEquals(villas[0]));
        }

        [Fact]
        public async Task GetVillasReturnsCorrectVillasWithStartDateParameter()
        {
            await _repo.AddVillaAsync(_villa1);
            await _repo.AddVillaAsync(_villa2);
            var reservation = new Reservation { Id = 1, VillaId = 1, UserId = 1, StartDate = _todayDate, EndDate = _todayDate };
            await _repo.AddReservationAsync(reservation);
            _filterParameters = new VillaFilterParameters { StartDate = _todayDate };

            var villas = (await _villaHandler.GetVillasAsync(_filterParameters)).ToList();

            Assert.Single(villas);
            Assert.True(_villa2.ValueEquals(villas[0]));
        }

        [Fact]
        public async Task GetVillasReturnsCorrectVillasWithEndDateParameter()
        {
            await _repo.AddVillaAsync(_villa1);
            await _repo.AddVillaAsync(_villa2);
            var reservation = new Reservation { Id = 1, VillaId = 1, UserId = 1, StartDate = _todayDate, EndDate = _todayDate };
            await _repo.AddReservationAsync(reservation);
            _filterParameters = new VillaFilterParameters { EndDate = _todayDate };

            var villas = (await _villaHandler.GetVillasAsync(_filterParameters)).ToList();

            Assert.Single(villas);
            Assert.True(_villa2.ValueEquals(villas[0]));
        }

        [Fact]
        public async Task GetVillasReturnsCorrectVillasWithStartDateAndEndDateParameters()
        {
            await _repo.AddVillaAsync(_villa1);
            await _repo.AddVillaAsync(_villa2);
            await _repo.AddVillaAsync(_villa3);
            var reservation1 = new Reservation { Id = 1, VillaId = 1, UserId = 1, StartDate = _todayDate, EndDate = _todayDate };
            var reservation2 = new Reservation { Id = 2, VillaId = 2, UserId = 1, StartDate = _todayDate.AddDays(1), EndDate = _todayDate.AddDays(1) };
            await _repo.AddReservationAsync(reservation1);
            await _repo.AddReservationAsync(reservation2);
            _filterParameters = new VillaFilterParameters { StartDate = _todayDate, EndDate = _todayDate.AddDays(1) };

            var villas = (await _villaHandler.GetVillasAsync(_filterParameters)).ToList();

            Assert.Single(villas);
            Assert.True(_villa3.ValueEquals(villas[0]));
        }

        [Fact]
        public async Task GetVillaDetailsReturnsCorrectVillaDetailsWithNoReservedDates()
        {
            await _repo.AddVillaAsync(_villa1);

            var villaDetails = await _villaHandler.GetVillaDetailsAsync(1);

            Assert.Equal(_villa1, villaDetails.Villa);
            Assert.Empty(villaDetails.ReservedDates);
        }

        [Fact]
        public async Task GetVillaDetailsReturnsCorrectVillaDetailsWithReservedDates()
        {
            await _repo.AddVillaAsync(_villa1);
            var reservation = new Reservation { Id = 1, VillaId = 1, UserId = 1, StartDate = _todayDate, EndDate = _todayDate };
            await _repo.AddReservationAsync(reservation);

            var villaDetails = await _villaHandler.GetVillaDetailsAsync(1);

            Assert.Equal(_villa1, villaDetails.Villa);
            Assert.Single(villaDetails.ReservedDates);
            Assert.Equal(_todayDate, villaDetails.ReservedDates.First().StartDate);
            Assert.Equal(_todayDate, villaDetails.ReservedDates.First().EndDate);
        }

        [Fact]
        public async Task AddVillaWorksCorrectly()
        {
            await _villaHandler.AddVillaAsync(_villa1);

            Assert.Single(await _repo.GetVillasAsync());
            Assert.Equal(_villa1, await _repo.GetVillaAsync(1));
        }

        [Fact]
        public async Task DeleteVillaWorksCorrectly()
        {
            await _villaHandler.AddVillaAsync(_villa1);

            await _villaHandler.DeleteVillaAsync(1);

            Assert.Empty(await _repo.GetVillasAsync());
        }

        [Fact]
        public async Task GetNonExistentVillaThrowsNotFoundException()
        {
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => 
                _villaHandler.GetVillaDetailsAsync(1));
            Assert.Equal("No villa with this ID", exception.Message);
        }

        [Fact]
        public async Task DeleteNonExistentVillaThrowsNotFoundException()
        {
            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                _villaHandler.DeleteVillaAsync(1));
            Assert.Equal("No villa with this ID", exception.Message);
        }
    }
}
