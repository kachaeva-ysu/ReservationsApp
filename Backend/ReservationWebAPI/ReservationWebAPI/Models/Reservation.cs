using System;

namespace ReservationWebAPI
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VillaId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
