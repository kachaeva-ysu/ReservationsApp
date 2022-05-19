using System;

namespace ReservationWebAPI
{
    public class UsersReservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int VillaId { get; set; }
        public string VillaName { get; set; }
        private decimal PriceForDay { get; set; }
        public decimal TotalPrice => ((EndDate - StartDate).Days + 1) * PriceForDay;

        public UsersReservation(int id, DateTime startDate, DateTime endDate, int villaId, string villaName, decimal priceForDay)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            VillaId = villaId;
            VillaName = villaName;
            PriceForDay = priceForDay;
        }
    }
}
