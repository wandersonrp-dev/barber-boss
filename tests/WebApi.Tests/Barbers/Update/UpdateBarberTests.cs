using BarberBoss.Communication.Requests.Barber;
using BarberBoss.Domain.Entities;
using Common.Tests.Utilities.Requests.Barbers;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.Barbers.Update;
public class UpdateBarberTests : BarberBossClassFixture
{
    private const string METHOD = "/api/barbers/profile";
    private readonly Barber _barber;

    public UpdateBarberTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
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

        var barberShop = RequestBarberJsonBuilder.Build();

        var result = await PatchAsync(METHOD, new RequestBarberJson
        {
            Email = barberShop.Email,
            Name = barberShop.Name,
            Phone = barberShop.Phone,            
            Password = barberShop.Password,            
        }, token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
