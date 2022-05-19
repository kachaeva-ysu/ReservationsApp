using Google.Apis.Auth;
using System;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public class GoogleAuthorizationHandler: IGoogleAuthorizationHandler
    {
        private string _googleClientId;
        private string _googleHostedDomain;

        public GoogleAuthorizationHandler(string googleClientId, string googleHostedDomain)
        {
            _googleClientId = googleClientId;
            _googleHostedDomain = googleHostedDomain;
        }

        public async Task<bool> CheckIfGoogleTokenIsValid(string token)
        {
            try
            {
                var payload = await GetPayload(token);
                if (!CheckIfGoogleTokenIsOriginal(payload))
                    return false;
                if (!CheckIfGoogleTokenIsUnexpired(payload.ExpirationTimeSeconds))
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetUserNameFromGoogleToken(string token)
        {
            var payload = await GetPayload(token);
            return payload.Name;
        }

        public async Task<string> GetEmailFromGoogleToken(string token)
        {
            var payload = await GetPayload(token);
            return payload.Email;
        }

        private async Task<GoogleJsonWebSignature.Payload> GetPayload(string token)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(token);
            return payload;
        }

        private bool CheckIfGoogleTokenIsOriginal(GoogleJsonWebSignature.Payload payload)
        {
            if (!payload.Audience.Equals(_googleClientId))
                return false;
            if (!payload.Issuer.Equals(_googleHostedDomain))
                return false;
            return true;
        }

        private bool CheckIfGoogleTokenIsUnexpired(long? expirationTime)
        {
            if (expirationTime == null)
                return false;
            var now = DateTime.Now.ToUniversalTime();
            var exp = DateTimeOffset.FromUnixTimeSeconds(expirationTime.Value).DateTime;
            if (now > exp)
                return false;
            return true;
        }
    }
}
