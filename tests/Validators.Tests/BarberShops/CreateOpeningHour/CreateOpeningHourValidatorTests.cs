using BarberBoss.Communication.Validators.BarberShop;
using BarberBoss.Exception;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;

namespace Validators.Tests.BarberShops.CreateOpeningHour;
public class CreateOpeningHourValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new CreateOpeningHourValidator();

        var request = RequestCreateOpeningHourJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }    

    [Fact]
    public void Error_Invalid_Start_Date()
    {
        var validator = new CreateOpeningHourValidator();

        var request = RequestCreateOpeningHourJsonBuilder.Build(startDate: DateTime.UtcNow.AddDays(-1));

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.INVALID_START_DATE));
    }

    [Fact]
    public void Error_Invalid_End_Date()
    {
        var validator = new CreateOpeningHourValidator();

        var request = RequestCreateOpeningHourJsonBuilder.Build(startDate: DateTime.UtcNow.AddMinutes(5), endDate: DateTime.UtcNow.AddMinutes(-50));

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.INVALID_END_DATE));
    }
}
