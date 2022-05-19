using System;

namespace ReservationWebAPI
{
    public class ForbiddenException: Exception
    {
        public ForbiddenException(string message) : base(message) { }
    }
}
