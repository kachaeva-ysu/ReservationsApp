using System.Collections.Generic;

namespace ReservationWebAPI
{
    public class VillaDetails
    {
        public Villa Villa { get; set; }
        public IEnumerable<ReservationDates> ReservedDates { get; set; }
    }
}
