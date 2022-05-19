using FluentValidation;
using System.Text.RegularExpressions;

namespace ReservationWebAPI.Validators
{
    public class UserInfoForUpdateValidator: AbstractValidator<UserInfoForUpdate>
    {
        private Regex _phoneRegex = new Regex(@"^\+?\d{6,15}$");

        public UserInfoForUpdateValidator()
        {
            RuleFor(u => u["name"]).NotEmpty().When(u => u.ContainsKey("name")).WithMessage("Invalid name");
            RuleFor(u => u["phone"]).Must(p => p != null && _phoneRegex.IsMatch(p)).When(u => u.ContainsKey("phone")).WithMessage("Invalid phone");
            RuleFor(u => u["password"]).Must(p => p != null && p.Length >= 8).When(u => u.ContainsKey("password")).WithMessage("Invalid password");
        }
    }
}
