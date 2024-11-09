using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.BarberShops.CreateBarber;
public class CreateBarberTests : BarberBossClassFixture
{    
    private const string METHOD = "api/barber-shops/barbers";
    private readonly BarberShop _barberShop;

    public CreateBarberTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {        
        _barberShop = webApplicationFactory.GetBarberShop();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestCreateBarberJsonBuilder.Build();

        var resultLogin = await PostAsync("api/barber-shops/signin", new RequestBarberShopDoLoginJson
        {
            Email = _barberShop.Email,
            Password = _barberShop.Password,
        });

        resultLogin.StatusCode.Should().Be(HttpStatusCode.OK);

        var bodyLogin = await resultLogin.Content.ReadAsStreamAsync();

        var responseLogin = await JsonDocument.ParseAsync(bodyLogin);

        var token = responseLogin.RootElement.GetProperty("token").GetString();        

        var result = await PostAsync(METHOD, request, token);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("isCreated").GetBoolean().Should().Be(true);
    }

    [Fact]
    public async Task Error_Name_Required()
    {
        var request = RequestCreateBarberJsonBuilder.Build();
        request.Name = string.Empty;

        var resultLogin = await PostAsync("api/barber-shops/signin", new RequestBarberShopDoLoginJson
        {
            Email = _barberShop.Email,
            Password = _barberShop.Password,
        });

        resultLogin.StatusCode.Should().Be(HttpStatusCode.OK);

        var bodyLogin = await resultLogin.Content.ReadAsStreamAsync();

        var responseLogin = await JsonDocument.ParseAsync(bodyLogin);

        var token = responseLogin.RootElement.GetProperty("token").GetString();        

        var result = await PostAsync(METHOD, request, token);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(errorMessage => errorMessage.GetString()!.Equals(ResourceErrorMessages.REQUIRED_NAME));
    }

    [Fact]
    public async Task Error_Barber_Already_Exists()
    {
        var request = RequestCreateBarberJsonBuilder.Build();      
        
        var resultLogin = await PostAsync("api/barber-shops/signin", new RequestBarberShopDoLoginJson
        {
            Email = _barberShop.Email,
            Password = _barberShop.Password,
        });

        resultLogin.StatusCode.Should().Be(HttpStatusCode.OK);

        var bodyLogin = await resultLogin.Content.ReadAsStreamAsync();

        var responseLogin = await JsonDocument.ParseAsync(bodyLogin);

        var token = responseLogin.RootElement.GetProperty("token").GetString();        

        await PostAsync(METHOD, request, token);

        var result = await PostAsync(METHOD, request, token);

        result.StatusCode.Should().Be(HttpStatusCode.Conflict);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(errorMessage => errorMessage.GetString()!.Equals(ResourceErrorMessages.BARBER_ALREADY_EXISTS));
    }
}
