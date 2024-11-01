﻿using BarberBoss.Communication.Validators.BarberShop;
using BarberBoss.Exception;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;

namespace Validators.Tests.BarberShops.Register;
public class RegisterBarberShopValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterBarberShopValidator();

        var request = RequestRegisterBarberShopJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Required_Name()
    {
        var validator = new RegisterBarberShopValidator();

        var request = RequestRegisterBarberShopJsonBuilder.Build();
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
        var validator = new RegisterBarberShopValidator();

        var request = RequestRegisterBarberShopJsonBuilder.Build();
        
        request.Name = string.Create(length: nameLength, state: '\0', (span, state) =>
        {
            for(var i = 0; i < span.Length; i++)
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
        var validator = new RegisterBarberShopValidator();

        var request = RequestRegisterBarberShopJsonBuilder.Build();
        request.Email = email;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.INVALID_EMAIL));
    }

    [Fact]
    public void Error_Required_Email()
    {
        var validator = new RegisterBarberShopValidator();

        var request = RequestRegisterBarberShopJsonBuilder.Build();
        request.Email = default!;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.REQUIRED_EMAIL));
    }

    [Fact]
    public void Error_Required_Phone()
    {
        var validator = new RegisterBarberShopValidator();

        var request = RequestRegisterBarberShopJsonBuilder.Build();
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
        var validator = new RegisterBarberShopValidator();

        var request = RequestRegisterBarberShopJsonBuilder.Build();
        request.Phone = phone;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PHONE_MAX_LENGTH));
    }

    [Fact]
    public void Error_Required_Phone_Contact()
    {
        var validator = new RegisterBarberShopValidator();

        var request = RequestRegisterBarberShopJsonBuilder.Build();
        request.PhoneContact = default!;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.REQUIRED_PHONE_CONTACT));
    }

    [Theory]
    [InlineData(151)]
    [InlineData(185)]
    public void Error_Phone_Contact_Max_Length(int nameLength)
    {
        var validator = new RegisterBarberShopValidator();

        var request = RequestRegisterBarberShopJsonBuilder.Build();

        request.PhoneContact = string.Create(length: nameLength, state: '\0', (span, state) =>
        {
            for (var i = 0; i < span.Length; i++)
            {
                span[i] = 'a';
            }
        });

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PHONE_CONTACT_MAX_LENGTH));
    }

    [Fact]
    public void Error_Required_Password()
    {
        var validator = new RegisterBarberShopValidator();

        var request = RequestRegisterBarberShopJsonBuilder.Build();
        request.Password = default!;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.REQUIRED_PASSWORD));
    }

    [Theory]
    [InlineData("1234567")]
    [InlineData("12345")]
    [InlineData("123")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("1234")]
    public void Error_Password_Min_Length(string password)
    {
        var validator = new RegisterBarberShopValidator();

        var request = RequestRegisterBarberShopJsonBuilder.Build();
        request.Password = password;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PASSWORD_MIN_LENGTH));
    }

    [Theory]
    [InlineData(33)]
    [InlineData(50)]
    public void Error_Password_Max_Length(int nameLength)
    {
        var validator = new RegisterBarberShopValidator();

        var request = RequestRegisterBarberShopJsonBuilder.Build();

        request.Password = string.Create(length: nameLength, state: '\0', (span, state) =>
        {
            for (var i = 0; i < span.Length; i++)
            {
                span[i] = 'a';
            }
        });

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PASSWORD_MAX_LENGTH));
    }    
}