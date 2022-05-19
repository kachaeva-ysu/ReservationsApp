using System;

namespace ReservationWebAPI
{
    public class VillaFilterParameters
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? MinPriceForDay { get; set; }
        public decimal? MaxPriceForDay { get; set; }
        public int? MinNumberOfRooms { get; set; }
        public int? MaxNumberOfRooms { get; set; }
        public bool? HasPool { get; set; }
    }
}