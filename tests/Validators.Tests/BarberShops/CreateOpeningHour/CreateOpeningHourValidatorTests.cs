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

        var collection = RequestCreateOpeningHourJsonBuilder.Collection();

        var request = RequestCreateOpeningHourJsonBuilder.Build(collection);

        foreach (var openingHour in request.OpeningHours)
        {
            var result = validator.Validate(openingHour);

            result.IsValid.Should().BeTrue();
        }
    }

    [Fact]
    public void Error_Invalid_Start_Date()
    {
        var validator = new CreateOpeningHourValidator();

        var collection = RequestCreateOpeningHourJsonBuilder.Collection(startDate: DateTime.UtcNow.AddMinutes(-5));

        var request = RequestCreateOpeningHourJsonBuilder.Build(collection);

        foreach (var openingHour in request.OpeningHours)
        {
            var result = validator.Validate(openingHour);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.INVALID_START_DATE));
        }

    }

    [Fact]
    public void Error_Invalid_End_Date()
    {
        var validator = new CreateOpeningHourValidator();

        var collection = RequestCreateOpeningHourJsonBuilder.Collection(startDate: DateTime.UtcNow.AddMinutes(5), endDate: DateTime.UtcNow.AddMinutes(-50));

        var request = RequestCreateOpeningHourJsonBuilder.Build(collection);

        foreach(var openingHour in request.OpeningHours)
        {
            var result = validator.Validate(openingHour);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.INVALID_END_DATE));
        }                        
    }
}
