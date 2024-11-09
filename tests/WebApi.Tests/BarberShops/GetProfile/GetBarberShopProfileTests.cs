using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Domain.Entities;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.BarberShops.GetProfile;
public class GetBarberShopProfileTests : BarberBossClassFixture
{
    private const string METHOD = "api/barber-shops/profile";    
    private readonly BarberShop _barberShop;

    public GetBarberShopProfileTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory) 
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

        var result = await GetAsync(METHOD, token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("email").GetString().Should().Be(_barberShop.Email);
    }
}
