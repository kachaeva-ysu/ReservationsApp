using Xunit;
using System.Collections.Generic;
using System;

namespace ReservationWebAPI.UnitTests
{
    public class DatesHandlerTests
    {
        ReservationDates _reservationDates;
        Reservation _reservation;
        public DatesHandlerTests()
        {
            var todayDate = DateTime.Now;
            _reservationDates = new ReservationDates(todayDate, todayDate);
            _reservation = new Reservation { VillaId = 1, StartDate = todayDate.AddDays(1), EndDate = todayDate.AddDays(2) };
        }

        [Fact]
        public void CheckIfDatesAreAvailableReturnsTrueWithEmptyReservations()
        {
            var reservations = new List<Reservation>();

            var res = ReservationDatesHandler.CheckIfDatesAreAvailable(1, reservations, _reservationDates);

            Assert.True(res);
        }

        [Fact]
        public void CheckIfDatesAreAvailableReturnsTrueIfDatesAreAvailable()
        {
            var reservations = new List<Reservation> { _reservation };

            var res = ReservationDatesHandler.CheckIfDatesAreAvailable(1, reservations, _reservationDates);

            Assert.True(res);
        }

        [Fact]
        public void CheckIfDatesAreAvailableReturnsFalseIfDatesAreNotAvailable()
        {
            _reservation.StartDate = _reservation.StartDate.AddDays(-2);
            var reservations = new List<Reservation> { _reservation };

            var res = ReservationDatesHandler.CheckIfDatesAreAvailable(1, reservations, _reservationDates);

            Assert.False(res);
        }
    }
}

