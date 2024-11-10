using BarberBoss.Communication.Requests.Barber;
using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using Common.Tests.Utilities.Requests.Barbers;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.Barbers.DoLogin;
public class BarberDoLoginTests : BarberBossClassFixture
{
    private const string METHOD = "api/barbers/signin";
    private readonly Barber _barber; 

    public BarberDoLoginTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _barber = webApplicationFactory.GetBarber();
    }

    [Fact]
    public async Task Success()
    {                
        var result = await PostAsync(METHOD, new RequestBarberDoLoginJson
        {
            Email = _barber.Email,
            Password = _barber.Password,
        });
               
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Email_Invalid_Credentials()
    {
        var result = await PostAsync(METHOD, new RequestBarberDoLoginJson
        {
            Email = _barber.Email,
            Password = string.Empty,
        });                        

        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(errorMessage => errorMessage.GetString()!.Equals(ResourceErrorMessages.INVALID_CREDENTIALS));
    }
}
