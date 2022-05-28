namespace ReservationWebAPI
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }

        public bool ValueEquals(User user)
        {
            if(Id== user.Id && Name==user.Name && Phone==user.Phone && Email==user.Email 
                && Password == user.Password)
                    return true;
            return false;
        }
    }
}
