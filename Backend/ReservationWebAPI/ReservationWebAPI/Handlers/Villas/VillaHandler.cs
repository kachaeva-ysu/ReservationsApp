using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public class VillaHandler : IVillaHandler
    {
        private readonly IAppRepository _repo;

        public VillaHandler(IAppRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Villa>> GetVillasAsync(VillaFilterParameters parameters)
        {
            var filteredVillas = new List<Villa>();
            var villas = await _repo.GetVillasAsync();
            foreach (var villa in villas)
            {
                if (await CheckIfVillaSatisfiesFilterParametersAsync(villa, parameters))
                    filteredVillas.Add(villa);
            }

            return filteredVillas;
        }

        public async Task<VillaDetails> GetVillaDetailsAsync(int villaId)
        {
            var villa = await FindVillaAsync(villaId);
            var dates = await GetReservedDatesAsync(villaId);
            return new VillaDetails { Villa = villa, ReservedDates = dates };
        }

        public async Task AddVillaAsync(Villa villa)
        {
            await _repo.AddVillaAsync(villa);
        }

        public async Task DeleteVillaAsync(int villaId)
        {
            var villa = await FindVillaAsync(villaId);
            await _repo.DeleteVillaAsync(villa);
        }

        private async Task<Villa> FindVillaAsync(int villaId)
        {
            var villa = await _repo.GetVillaAsync(villaId);
            if (villa == null)
                throw new NotFoundException("No villa with this ID");

            return villa;
        }

        private async Task<bool> CheckIfVillaSatisfiesFilterParametersAsync(Villa villa, VillaFilterParameters parameters)
        {
            if (CheckIfVillaSatisfiesPoolParameter(villa.HasPool, parameters.HasPool) && 
                CheckIfVillaSatisfiesMinNumberOfRoomsParameter(villa.NumberOfRooms, parameters.MinNumberOfRooms) &&
                CheckIfVillaSatisfiesMaxNumberOfRoomsParameter(villa.NumberOfRooms, parameters.MaxNumberOfRooms) &&
                CheckIfVillaSatisfiesMinPriceForDayParameter(villa.PriceForDay, parameters.MinPriceForDay) &&
                CheckIfVillaSatisfiesMaxPriceForDayParameter(villa.PriceForDay, parameters.MaxPriceForDay) &&
                await CheckIfVillaIsAvailableAsync(villa.Id, parameters.StartDate, parameters.EndDate))
                return true;
            return false;
        }

        private bool CheckIfVillaSatisfiesPoolParameter(bool villaHasPool, bool? parametersHasPool)
        {
            if (parametersHasPool.HasValue && villaHasPool == parametersHasPool || !parametersHasPool.HasValue)
                return true;
            return false;
        }

        private bool CheckIfVillaSatisfiesMinNumberOfRoomsParameter(int villaNumberOfRooms, int? minNumberOfRooms)
        {
            if (minNumberOfRooms.HasValue && villaNumberOfRooms >= minNumberOfRooms || !minNumberOfRooms.HasValue)
                return true;
            return false;
        }

        private bool CheckIfVillaSatisfiesMaxNumberOfRoomsParameter(int villaNumberOfRooms, int? maxNumberOfRooms)
        {
            if (maxNumberOfRooms.HasValue && villaNumberOfRooms <= maxNumberOfRooms || !maxNumberOfRooms.HasValue)
                return true;
            return false;
        }

        private bool CheckIfVillaSatisfiesMinPriceForDayParameter(decimal villaPriceForDay, decimal? minPriceForDay)
        {
            if (minPriceForDay.HasValue && villaPriceForDay >= minPriceForDay || !minPriceForDay.HasValue)
                return true;
            return false;
        }

        private bool CheckIfVillaSatisfiesMaxPriceForDayParameter(decimal villaPriceForDay, decimal? maxPriceForDay)
        {
            if (maxPriceForDay.HasValue && villaPriceForDay <= maxPriceForDay || !maxPriceForDay.HasValue)
                return true;
            return false;
        }

        private async Task<bool> CheckIfVillaIsAvailableAsync(int villaId, DateTime? startDateParameter, DateTime? endDateParameter)
        {
            if (!startDateParameter.HasValue && !endDateParameter.HasValue)
                return true;

            var reservationDates = CreateReservationDatesFromFilterParameters(startDateParameter, endDateParameter);
            var reservations = await _repo.GetReservationsAsync();
            return ReservationDatesHandler.CheckIfDatesAreAvailable(villaId, reservations, reservationDates);
        }

        private ReservationDates CreateReservationDatesFromFilterParameters(DateTime? startDateParameter, DateTime? endDateParameter)
        {
            var startDate = (DateTime)(startDateParameter.HasValue ? startDateParameter : endDateParameter);
            var endDate = (DateTime)(endDateParameter.HasValue ? endDateParameter : startDateParameter);
            return new ReservationDates(startDate, endDate);
        }

        private async Task<IEnumerable<ReservationDates>> GetReservedDatesAsync(int villaId)
        {
            var dates = new List<ReservationDates>();
            var reservations = await _repo.GetReservationsAsync();
            foreach (var reservation in reservations)
            {
                if (villaId == reservation.VillaId)
                    dates.Add(new ReservationDates(reservation.StartDate, reservation.EndDate));
            }

            return dates;
        }
    }
}
