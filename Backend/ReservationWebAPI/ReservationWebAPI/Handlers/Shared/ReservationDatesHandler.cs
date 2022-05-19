using System;
using System.Collections.Generic;

namespace ReservationWebAPI
{
    public static class ReservationDatesHandler
    {
        public static bool CheckIfDatesAreAvailable(int villaId, IEnumerable<Reservation> reservations, ReservationDates dates)
        {
            foreach (var reservation in reservations) 
            {
                if (reservation.VillaId == villaId)
                {
                    if (!(dates.StartDate > reservation.EndDate || dates.EndDate < reservation.StartDate))
                        return false;
                }
            }
            return true;
        }
    }
}
