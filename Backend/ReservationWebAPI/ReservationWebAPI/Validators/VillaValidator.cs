using FluentValidation;

namespace ReservationWebAPI
{
    public class VillaValidator : AbstractValidator<Villa>
    {
        private string _message = "Invalid {PropertyName}";

        public VillaValidator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage(_message);
            RuleFor(v => v.PriceForDay).GreaterThan(0).WithMessage(_message);
            RuleFor(v => v.NumberOfRooms).GreaterThan(0).WithMessage(_message);
        }
    }
}
