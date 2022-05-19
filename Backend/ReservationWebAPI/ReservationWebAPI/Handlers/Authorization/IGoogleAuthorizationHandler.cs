using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public interface IGoogleAuthorizationHandler
    {
        public Task<bool> CheckIfGoogleTokenIsValid(string token);
        public Task<string> GetUserNameFromGoogleToken(string token);
        public Task<string> GetEmailFromGoogleToken(string token);
    }
}
