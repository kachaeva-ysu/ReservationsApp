using System;

namespace ReservationWebAPI
{
    public class ReservationDates
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ReservationDates(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
