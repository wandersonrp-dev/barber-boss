using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Domain.Entities;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Tests.BarberShops.Update;
public class UpdateBarberShopTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private const string METHOD = "/api/barber-shops/profile"; 
    private readonly BarberShop _barberShop;

    public UpdateBarberShopTests(CustomWebApplicationFactory webApplicationFactory)
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

        var barberShop = RequestBarberShopJsonBuilder.Build();

        var result = await _httpClient.PatchAsJsonAsync(METHOD, new RequestBarberShopJson
        {
            Email= barberShop.Email,            
            Name = barberShop.Name,
            Phone = barberShop.Phone,
            PhoneContact = barberShop.PhoneContact,
            Password = barberShop.Password,            

        });

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);        
    }   
}
