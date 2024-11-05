using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Domain.Entities;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Tests.BarberShops.GetProfile;
public class GetBarberShopProfileTests : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "api/barber-shops/perfil";
    private readonly HttpClient _httpClient;
    private readonly BarberShop _barberShop;


    public GetBarberShopProfileTests(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
        _barberShop = webApplicationFactory.GetBarberShop();
    }

    [Fact]
    public async Task Success()
    {
        var resultLogin = await _httpClient.PostAsJsonAsync("api/barber-shops/signin", new RequestBarberShopDoLoginJson
        {
            Email = _barberShop.Email,
            Password = _barberShop.Password,
        });

        resultLogin.StatusCode.Should().Be(HttpStatusCode.OK);

        var bodyLogin = await resultLogin.Content.ReadAsStreamAsync();

        var responseLogin = await JsonDocument.ParseAsync(bodyLogin);

        var token = responseLogin.RootElement.GetProperty("token").GetString();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var result = await _httpClient.GetAsync(METHOD);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("email").GetString().Should().Be(_barberShop.Email);
    }
}
