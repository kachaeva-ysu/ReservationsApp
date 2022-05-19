using FluentValidation;

namespace ReservationWebAPI
{
    public class VillaValidator : AbstractValidator<Villa>
    {
        private string _message = "{PropertyName} cannot be empty";

        public VillaValidator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage(_message);
            RuleFor(v => v.PriceForDay).NotEmpty().GreaterThan(0).WithMessage(_message);
            RuleFor(v => v.NumberOfRooms).NotEmpty().GreaterThan(0).WithMessage(_message);
        }
    }
}
