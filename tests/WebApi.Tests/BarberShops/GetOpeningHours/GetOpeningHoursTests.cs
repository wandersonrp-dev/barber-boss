using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Domain.Entities;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.BarberShops.GetOpeningHours;
public class GetOpeningHoursTests : BarberBossClassFixture
{
    private const string METHOD = "api/barber-shops/opening-hours";
    private readonly OpeningHour _openingHour;
    private readonly BarberShop _barberShop;

    public GetOpeningHoursTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _openingHour = webApplicationFactory.GetOpeningHour();
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

        var date = DateTime.Now.AddMinutes(10);

        var dateString = date.ToString("yyyy-MM-ddTHH:mm:ss");

        var collection = RequestCreateOpeningHourJsonBuilder.Collection(count: 1, startDate: date);

        var request = RequestCreateOpeningHourJsonBuilder.Build(collection);

        await PostAsync(METHOD, request, token);

        var url = $"{METHOD}/{dateString}";

        var result = await GetAsync(url, token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var openingHours = response.RootElement.GetProperty("openingHours").EnumerateArray();

        openingHours.Should().HaveCount(1);
    }
}
