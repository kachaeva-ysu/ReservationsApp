namespace ReservationWebAPI
{
    public interface IUserInfoFromToken
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
