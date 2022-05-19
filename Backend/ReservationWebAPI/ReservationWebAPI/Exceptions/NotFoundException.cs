using System;

namespace ReservationWebAPI
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
