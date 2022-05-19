namespace ReservationWebAPI
{
    public interface IPasswordHandler
    {
        public string GetHashedPassword(string password, string email);
    }
}
