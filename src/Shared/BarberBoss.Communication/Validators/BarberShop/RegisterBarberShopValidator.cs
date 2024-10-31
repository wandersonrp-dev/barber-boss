using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Communication.Validators.BarberShop;
public class RegisterBarberShopValidator : AbstractValidator<RequestRegisterBarberShopJson>
{
    public RegisterBarberShopValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.REQUIRED_NAME)
            .MaximumLength(150)
            .WithMessage(ResourceErrorMessages.NAME_MAX_LENGTH);

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage(ResourceErrorMessages.INVALID_EMAIL);

        RuleFor(x => x.PhoneContact)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.REQUIRED_PHONE_CONTACT)
            .MaximumLength(150)
            .WithMessage(ResourceErrorMessages.PHONE_CONTACT_MAX_LENGTH);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.REQUIRED_PHONE)
            .Matches(@"^\d{10,11}$")
            .WithMessage(ResourceErrorMessages.PHONE_MAX_LENGTH);

        RuleFor(x => x.Password)            
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.REQUIRED_PASSWORD)
            .MinimumLength(8)
            .WithMessage(ResourceErrorMessages.PASSWORD_MIN_LENGTH)
            .MinimumLength(32)
            .WithMessage(ResourceErrorMessages.PASSWORD_MAX_LENGTH);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage(ResourceErrorMessages.CONFIRM_PASSWORD_DOES_NOT_MATCH);
    }               
}
