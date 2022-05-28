namespace ReservationWebAPI
{
    public class UserInfoFromToken: IUserInfoFromToken
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string RefreshToken { get; set; }
    }
}
