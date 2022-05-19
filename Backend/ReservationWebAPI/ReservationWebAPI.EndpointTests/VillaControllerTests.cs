using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace ReservationWebAPI.EndpointTests
{
    [Collection("Client collection")]
    public class VillaControllerTests: IClassFixture<ClientFixture>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly IAppRepository _repo;
        private readonly string _uri = "villas";
        private readonly Villa _testVilla = new Villa { Description = "Descr1", HasPool = true, Name = "Villa1", NumberOfRooms = 2, PriceForDay = 200 };

        public VillaControllerTests(ClientFixture fixture)
        {
            _client = fixture.Client;
            _repo = fixture.AppRepository;
        }

        public void Dispose()
        {
            var villas = _repo.GetVillasAsync().Result;
            foreach (var villa in villas)
                _repo.DeleteVillaAsync(villa).Wait();
            var reservations = _repo.GetReservationsAsync().Result;
            foreach (var reservation in reservations)
                _repo.DeleteReservationAsync(reservation).Wait();
        }

        [Fact]
        public async Task GetVillasReturnsOkAndEmptyVillasWithEmptyVillas()
        {
            var response = await _client.GetAsync(_uri);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var villas = response.Content.ReadFromJsonAsync<List<Villa>>().Result;

            Assert.Empty(villas);
        }

        [Fact]
        public async Task GetVillasReturnsOkAndVillasWithNotEmptyVillas()
        {
            await _repo.AddVillaAsync(_testVilla);

            var response = await _client.GetAsync(_uri);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var villas = response.Content.ReadFromJsonAsync<List<Villa>>().Result;

            Assert.Single(villas);
            Assert.True(_testVilla.ValueEquals(villas[0]));
        }

        [Fact]
        public async Task GetVillaDetailsReturnsOkAndVillaDetailsWithNoReservedDates()
        {
            await _repo.AddVillaAsync(_testVilla);

            var response = await _client.GetAsync($"{_uri}/{_testVilla.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var villaDetails = response.Content.ReadFromJsonAsync<VillaDetails>().Result;

            Assert.True(_testVilla.ValueEquals(villaDetails.Villa));
            Assert.Empty(villaDetails.ReservedDates);
        }

        [Fact]
        public async Task GetVillaDetailsReturnsOkAndVillaDetailsWithReservedDates()
        {
            await _repo.AddVillaAsync(_testVilla);
            var reservation = new Reservation { VillaId = _testVilla.Id, StartDate = DateTime.Now, EndDate = DateTime.Now, UserId = 1 };
            await _repo.AddReservationAsync(reservation);

            var response = await _client.GetAsync($"{_uri}/{_testVilla.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var villaDetails = response.Content.ReadFromJsonAsync<VillaDetails>().Result;

            Assert.True(_testVilla.ValueEquals(villaDetails.Villa));
            Assert.Single(villaDetails.ReservedDates);
        }

        [Fact]
        public async Task PostVillaReturnsCreatedAndVilla()
        {
            var response = await _client.PostAsync(_uri, StringContentMaker.GetBody(_testVilla));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var villa = response.Content.ReadFromJsonAsync<Villa>().Result;
            _testVilla.Id = villa.Id;

            Assert.True(_testVilla.ValueEquals(villa));
        }

        [Fact]
        public async Task DeleteVillaReturnsNoContent()
        {
            await _repo.AddVillaAsync(_testVilla);

            var response = await _client.DeleteAsync($"{_uri}/{_testVilla.Id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task GetVillaReturnsBadRequestIfFilterParametersAreInvalid()
        {
            var response = await _client.GetAsync($"{_uri}?minNumberOfRooms=-1");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostVillaReturnsBadRequestAndDoesNotAddVillaIfVillaIsInvalid()
        {
            var invalidVilla = new Villa { NumberOfRooms = -1 };

            var response = await _client.PostAsync(_uri, StringContentMaker.GetBody(invalidVilla));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Empty(await _repo.GetVillasAsync());
        }

        [Fact]
        public async Task GetVillaReturnsNotFoundAndMessageIfThereIsNoVillaWithRequestedId()
        {
            var response = await _client.GetAsync($"{_uri}/1");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("No villa with this ID", message);
        }

        [Fact]
        public async Task DeleteVillaReturnsNotFoundAndMessageIfThereIsNoVillaWithRequestedId()
        {
            var response = await _client.DeleteAsync($"{_uri}/1");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var message = response.Content.ReadAsStringAsync().Result;

            Assert.Equal("No villa with this ID", message);
        }
    }
}