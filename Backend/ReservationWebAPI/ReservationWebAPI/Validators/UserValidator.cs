using FluentValidation;
using System.Text.RegularExpressions;

namespace ReservationWebAPI
{
    public class UserValidator : AbstractValidator<User>
    {
        private Regex _phoneRegex = new Regex(@"^\+?\d{6,15}$");
        private Regex _emailRegex = new Regex(@"^\w+@\w+.\w+$");
        private string _message = "Invalid {PropertyName}";

        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage(_message);
            RuleFor(u => u.Phone).Must(p => p!=null && _phoneRegex.IsMatch(p)).WithMessage(_message);
            RuleFor(u => u.Email).Must(e => e!=null && _emailRegex.IsMatch(e)).WithMessage(_message);
            RuleFor(u => u.Password).Must(p => p!=null && p.Length>=8).WithMessage(_message);
        }
    }
}
