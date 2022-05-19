using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ReservationWebAPI
{
    public class TokenAuthorizationHandler: ITokenAuthorizationHandler
    {
        private string _secretKey;

        public TokenAuthorizationHandler(string secretKey)
        {
            _secretKey = secretKey;
        }

        public bool CheckIfTokenIsValidAndUnexpired(Token token)
        {
            if (!CheckIfTokenIsValid(token) || token.ExpirationTime < DateTime.Now)
                return false;
            return true;
        }

        public bool CheckIfTokenIsValid(Token token)
        {
            var blocks = token.AccessToken.Split('.');
            var header = blocks[0];
            var payload = blocks[1];
            var signature = blocks[2];

            var unsignedToken = header + '.' + payload;
            var validSignature = GetSignature(token.RefreshToken, unsignedToken);

            if (signature != validSignature)
                return false;

            return true;
        }

        public int GetUserIdFromToken(Token token)
        {
            var blocks = token.AccessToken.Split('.');
            var payload = blocks[1];
            return int.Parse(Encoding.ASCII.GetString(WebEncoders.Base64UrlDecode(payload)));
        }

        public UserAuthorizationInfo GetUserAuthorizationInfo(int userId)
        {
            var token = GetToken(userId);
            return new UserAuthorizationInfo { UserId=userId, Token=token };
        }
        
        public Token GetToken(int userId)
        {
            var refreshToken = GetRefreshToken();
            var header = GetHeader();
            var payload = GetPayload(userId);
            var unsignedToken = header + '.' + payload;
            var signature = GetSignature(refreshToken, unsignedToken);
            var accessToken = unsignedToken + '.' + signature;
            var token = new Token { AccessToken = accessToken, RefreshToken = refreshToken, ExpirationTime = DateTime.Now.AddDays(1) };
            return token;
        }
        
        private string GetRefreshToken()
        {
            var rnd = new Random();
            var refreshToken = new StringBuilder();

            while (refreshToken.Length < 50)
            {
                var symbol = (char)rnd.Next(48, 123);
                if (symbol >= '0' && symbol <= '9' || symbol >= 'a' && symbol <= 'z' || symbol >= 'A' && symbol <= 'Z')
                    refreshToken.Append(symbol);
            }

            return refreshToken.ToString();
        }

        private string GetHeader()
        {
            return WebEncoders.Base64UrlEncode(Encoding.ASCII.GetBytes("{ \"alg\":\"HS256\"}"));
        }

        private string GetPayload(int userId)
        {
            return WebEncoders.Base64UrlEncode(Encoding.ASCII.GetBytes(userId.ToString()));
        }

        private string GetSignature(string refreshToken, string unsignedToken)
        {
            var secretKey = Encoding.ASCII.GetBytes(_secretKey + refreshToken);
            var hash = new HMACSHA256(secretKey);
            return WebEncoders.Base64UrlEncode(hash.ComputeHash(Encoding.ASCII.GetBytes(unsignedToken)));
        }
    }
}
