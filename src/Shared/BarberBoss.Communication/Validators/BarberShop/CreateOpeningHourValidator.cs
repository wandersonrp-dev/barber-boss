using BarberBoss.Communication.Requests.BarberShop;
using FluentValidation;

namespace BarberBoss.Communication.Validators.BarberShop;
public class CreateOpeningHourValidator : AbstractValidator<RequestCreateOpeningHourJson>
{
    public CreateOpeningHourValidator()
    {
        RuleFor(x => x.StartDate).Cascade(CascadeMode.Stop).LessThan(DateTime.UtcNow).WithMessage("data/hora de início não pode ser menor que a data/hora atual");

        When(x => x.StartDate >= DateTime.UtcNow, () =>
        {
            RuleFor(x => x.StartDate).GreaterThan(x => x.EndDate).WithMessage("data/hora de início não pode ser maior que a data/final");
        });
    }
}
