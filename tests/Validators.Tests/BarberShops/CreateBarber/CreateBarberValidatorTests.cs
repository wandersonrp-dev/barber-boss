using BarberBoss.Communication.Validators.BarberShop;
using BarberBoss.Exception;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;

namespace Validators.Tests.BarberShops.CreateBarber;
public class CreateBarberValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new CreateBarberValidator();

        var request = RequestCreateBarberJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Required_Name()
    {
        var validator = new CreateBarberValidator();

        var request = RequestCreateBarberJsonBuilder.Build();
        request.Name = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.REQUIRED_NAME));
    }

    [Theory]
    [InlineData(151)]
    [InlineData(185)]
    public void Error_Name_Max_Length(int nameLength)
    {
        var validator = new CreateBarberValidator();

        var request = RequestCreateBarberJsonBuilder.Build();

        request.Name = string.Create(length: nameLength, state: '\0', (span, state) =>
        {
            for (var i = 0; i < span.Length; i++)
            {
                span[i] = 'a';
            }
        });

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.NAME_MAX_LENGTH));
    }

    [Theory]
    [InlineData("@example.com")]
    [InlineData("example.com")]
    public void Invalid_Email(string email)
    {
        var validator = new CreateBarberValidator();

        var request = RequestCreateBarberJsonBuilder.Build();
        request.Email = email;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.INVALID_EMAIL));
    }

    [Fact]
    public void Error_Required_Email()
    {
        var validator = new CreateBarberValidator();

        var request = RequestCreateBarberJsonBuilder.Build();
        request.Email = default!;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.REQUIRED_EMAIL));
    }

    [Fact]
    public void Error_Required_Phone()
    {
        var validator = new CreateBarberValidator();

        var request = RequestCreateBarberJsonBuilder.Build();
        request.Phone = default!;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.REQUIRED_PHONE));
    }

    [Theory]
    [InlineData("123456789")]
    [InlineData("123456789012")]
    [InlineData("12345")]
    [InlineData("12345678901234")]
    public void Error_Phone_Max_Length(string phone)
    {
        var validator = new CreateBarberValidator();

        var request = RequestCreateBarberJsonBuilder.Build();
        request.Phone = phone;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PHONE_MAX_LENGTH));
    }

    [Fact]
    public void Error_Required_Password()
    {
        var validator = new CreateBarberValidator();

        var request = RequestCreateBarberJsonBuilder.Build();
        request.Password = default!;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.REQUIRED_PASSWORD));
    }

    [Fact]
    public void Error_Confirm_Password_Does_Not_Match()
    {
        var validator = new CreateBarberValidator();

        var request = RequestCreateBarberJsonBuilder.Build(confirmPassword: "sdasjkdcjfbdxjhad451");        

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.CONFIRM_PASSWORD_DOES_NOT_MATCH));
    }
}
