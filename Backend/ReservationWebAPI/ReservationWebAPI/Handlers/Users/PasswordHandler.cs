using System;
using System.Security.Cryptography;
using System.Text;

namespace ReservationWebAPI
{
    public class PasswordHandler: IPasswordHandler
    {
        private string _salt;

        public PasswordHandler(string salt)
        {
            _salt = salt;
        }

        public string GetHashedPassword(string password, string email)
        {
            if (password == null)
                return null;
            var passwordWithSalt = $"{email}{password}{_salt}";
            var hash = SHA256.Create();
            var sourceBytes = Encoding.UTF8.GetBytes(passwordWithSalt);
            var hashBytes = hash.ComputeHash(hash.ComputeHash(sourceBytes));
            var hashedPassword = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            return hashedPassword;
        }
    }
}
