using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Communication.Validators.BarberShop;
public class BarberShopDoLoginValidator : AbstractValidator<RequestBarberShopDoLoginJson>
{
    public BarberShopDoLoginValidator()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.REQUIRED_EMAIL)
            .EmailAddress()
            .WithMessage(ResourceErrorMessages.INVALID_EMAIL);

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.REQUIRED_PASSWORD)
            .MinimumLength(8)
            .WithMessage(ResourceErrorMessages.PASSWORD_MIN_LENGTH)
            .MaximumLength(32)
            .WithMessage(ResourceErrorMessages.PASSWORD_MAX_LENGTH);
    }
}
