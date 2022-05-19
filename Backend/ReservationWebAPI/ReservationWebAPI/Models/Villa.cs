namespace ReservationWebAPI
{
    public class Villa
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal PriceForDay { get; set; }
        public int NumberOfRooms { get; set; }
        public bool HasPool { get; set; }
        public string Name { get; set; }

        public bool ValueEquals(Villa villa)
        {
            if (Id==villa.Id && Description == villa.Description && PriceForDay == villa.PriceForDay &&
               NumberOfRooms == villa.NumberOfRooms && HasPool == villa.HasPool && Name == villa.Name)
                return true;
            return false;
        }
    }
}

