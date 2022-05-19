namespace ReservationWebAPI
{
    public interface ITokenAuthorizationHandler
    {
        public bool CheckIfTokenIsValidAndUnexpired(Token token);
        public bool CheckIfTokenIsValid(Token token);
        public UserAuthorizationInfo GetUserAuthorizationInfo(int userId);
        public Token GetToken(int userId);
        public int GetUserIdFromToken(Token token);
    }
}
