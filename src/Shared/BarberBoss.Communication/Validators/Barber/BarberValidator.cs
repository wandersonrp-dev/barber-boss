﻿using BarberBoss.Communication.Requests.Barber;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Communication.Validators.Barber;
public class BarberValidator : AbstractValidator<RequestBarberJson>
{
    public BarberValidator()
    {
        When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
        {
            RuleFor(x => x.Name)
                .MaximumLength(150)
                .WithMessage(ResourceErrorMessages.NAME_MAX_LENGTH);
        });

        When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(ResourceErrorMessages.INVALID_EMAIL);
        });        

        When(x => !string.IsNullOrWhiteSpace(x.Phone), () =>
        {
            RuleFor(x => x.Phone)
                .Matches(@"^\d{10,11}$")
                .WithMessage(ResourceErrorMessages.PHONE_MAX_LENGTH);
        });

        When(x => !string.IsNullOrWhiteSpace(x.Password), () =>
        {
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .MinimumLength(8)
                .WithMessage(ResourceErrorMessages.PASSWORD_MIN_LENGTH)
                .MaximumLength(32)
                .WithMessage(ResourceErrorMessages.PASSWORD_MAX_LENGTH);
        });
    }
}
