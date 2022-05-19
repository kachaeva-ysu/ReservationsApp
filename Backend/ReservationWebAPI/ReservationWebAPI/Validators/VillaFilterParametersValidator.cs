using System;
using FluentValidation;

namespace ReservationWebAPI
{
    public class VillaFilterParametersValidator: AbstractValidator<VillaFilterParameters>
    {
        private string _message = "Invalid {PropertyName}";
        public VillaFilterParametersValidator()
        {
            RuleFor(p => p.StartDate).GreaterThanOrEqualTo(DateTime.Now.Date).When(p => p.StartDate.HasValue).WithMessage(_message);
            RuleFor(p => p.EndDate).GreaterThanOrEqualTo(DateTime.Now.Date).When(p => p.EndDate.HasValue).WithMessage(_message);
            RuleFor(p => p.MinNumberOfRooms).GreaterThanOrEqualTo(0).When(p => p.MinNumberOfRooms.HasValue).WithMessage(_message);
            RuleFor(p => p.MaxNumberOfRooms).GreaterThanOrEqualTo(0).When(p => p.MaxNumberOfRooms.HasValue).WithMessage(_message);
            RuleFor(p => p.MinPriceForDay).GreaterThanOrEqualTo(0).When(p => p.MinPriceForDay.HasValue).WithMessage(_message);
            RuleFor(p => p.MaxPriceForDay).GreaterThanOrEqualTo(0).When(p => p.MaxPriceForDay.HasValue).WithMessage(_message);
            RuleFor(p => p.EndDate).GreaterThanOrEqualTo(p => p.StartDate).When(p => p.StartDate.HasValue && p.EndDate.HasValue).WithMessage("End date cannot be earlier than start date");
            RuleFor(p => p.MaxPriceForDay).GreaterThanOrEqualTo(p => p.MinPriceForDay).When(p => p.MinPriceForDay.HasValue && p.MaxPriceForDay.HasValue).WithMessage("Max price for day cannot be less than min price for day");
            RuleFor(p => p.MaxNumberOfRooms).GreaterThanOrEqualTo(p => p.MinNumberOfRooms).When(p => p.MinNumberOfRooms.HasValue && p.MaxNumberOfRooms.HasValue).WithMessage("Max number of rooms cannot be less than min number of rooms");
        }
    }
}
