using System;

namespace ReservationWebAPI
{
    public class BadRequestException: Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
