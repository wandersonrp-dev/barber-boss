using BarberBoss.Communication.Requests;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Communication.Validators.BarberShop;
public class CreateOpeningHourValidator : AbstractValidator<RequestDateJson>
{
    public CreateOpeningHourValidator()
    {
        RuleFor(x => x.StartDate)            
            .GreaterThan(DateTime.UtcNow)
            .WithMessage(ResourceErrorMessages.INVALID_START_DATE);

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage(ResourceErrorMessages.INVALID_END_DATE);
    }
}
