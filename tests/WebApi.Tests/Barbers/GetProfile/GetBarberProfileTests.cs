using BarberBoss.Communication.Requests.Barber;
using BarberBoss.Domain.Entities;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.Barbers.GetProfile;
public class GetBarberProfileTests : BarberBossClassFixture
{
    private const string METHOD = "api/barbers/profile";
    private readonly Barber _barber;

    public GetBarberProfileTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _barber = webApplicationFactory.GetBarber();
    }

    [Fact]
    public async Task Success()
    {
        var resultLogin = await PostAsync("api/barbers/signin", new RequestBarberDoLoginJson
        {
            Email = _barber.Email,
            Password = _barber.Password,
        });

        resultLogin.StatusCode.Should().Be(HttpStatusCode.OK);

        var bodyLogin = await resultLogin.Content.ReadAsStreamAsync();

        var responseLogin = await JsonDocument.ParseAsync(bodyLogin);

        var token = responseLogin.RootElement.GetProperty("token").GetString();

        var result = await GetAsync(METHOD, token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("email").GetString().Should().Be(_barber.Email);
    }
}
