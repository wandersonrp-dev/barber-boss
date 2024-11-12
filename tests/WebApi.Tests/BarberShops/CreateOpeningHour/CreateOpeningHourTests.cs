using System.Net;
using System.Text.Json;
using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;

namespace WebApi.Tests.BarberShops.CreateOpeningHour;

public class CreateOpeningHourTests : BarberBossClassFixture
{
    private const string METHOD = "api/barber-shops/opening-hours";
    private readonly BarberShop _barberShop;

    public CreateOpeningHourTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _barberShop = webApplicationFactory.GetBarberShop();
    }

    [Fact]
    public async Task Success()
    {
        var resultLogin = await PostAsync("api/barber-shops/signin", new RequestBarberShopDoLoginJson
        {
            Email = _barberShop.Email,
            Password = _barberShop.Password,
        });

        resultLogin.StatusCode.Should().Be(HttpStatusCode.OK);

        var bodyLogin = await resultLogin.Content.ReadAsStreamAsync();

        var responseLogin = await JsonDocument.ParseAsync(bodyLogin);

        var token = responseLogin.RootElement.GetProperty("token").GetString();

        var request = RequestCreateOpeningHourJsonBuilder.Build();

        var result = await PostAsync(METHOD, request, token);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("isCreated").GetBoolean().Should().Be(true);         
    }

    [Fact]
    public async Task Error_On_Validation()
    {
         var resultLogin = await PostAsync("api/barber-shops/signin", new RequestBarberShopDoLoginJson
        {
            Email = _barberShop.Email,
            Password = _barberShop.Password,
        });

        resultLogin.StatusCode.Should().Be(HttpStatusCode.OK);

        var bodyLogin = await resultLogin.Content.ReadAsStreamAsync();

        var responseLogin = await JsonDocument.ParseAsync(bodyLogin);

        var token = responseLogin.RootElement.GetProperty("token").GetString();

        var request = RequestCreateOpeningHourJsonBuilder.Build();
        request.StartDate = DateTime.UtcNow.AddDays(-1);

        var result = await PostAsync(METHOD, request, token);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray(); 

        errors.Should().HaveCount(1).And.Contain(errorMessage => errorMessage.GetString()!.Equals(ResourceErrorMessages.INVALID_START_DATE));
    }

    [Fact]
    public async Task Error_Opening_Hours_Already_Exists()
    {
         var resultLogin = await PostAsync("api/barber-shops/signin", new RequestBarberShopDoLoginJson
        {
            Email = _barberShop.Email,
            Password = _barberShop.Password,
        });

        resultLogin.StatusCode.Should().Be(HttpStatusCode.OK);

        var bodyLogin = await resultLogin.Content.ReadAsStreamAsync();

        var responseLogin = await JsonDocument.ParseAsync(bodyLogin);

        var token = responseLogin.RootElement.GetProperty("token").GetString();

        var request = RequestCreateOpeningHourJsonBuilder.Build();        

        await PostAsync(METHOD, request, token);

        var result = await PostAsync(METHOD, request, token);

        result.StatusCode.Should().Be(HttpStatusCode.Conflict);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray(); 

        errors.Should().HaveCount(1).And.Contain(errorMessage => errorMessage.GetString()!.Equals(ResourceErrorMessages.OPENING_HOUR_ALREADY_EXISTS));
    }
}
