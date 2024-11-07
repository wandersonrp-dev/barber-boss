using BarberBoss.Communication.Requests.BarberShop;
using BarberBoss.Domain.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using FluentAssertions;
using Common.Tests.Utilities.Requests.BarberShops;
using BarberBoss.Exception;

namespace WebApi.Tests.BarberShops.CreateBarber;
public class CreateBarberTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private const string METHOD = "api/barber-shops/barbers";
    private readonly BarberShop _barberShop;

    public CreateBarberTests(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
        _barberShop = webApplicationFactory.GetBarberShop();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestCreateBarberJsonBuilder.Build();

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

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

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

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

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

        await _httpClient.PostAsJsonAsync(METHOD, request);

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.Conflict);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(errorMessage => errorMessage.GetString()!.Equals(ResourceErrorMessages.BARBER_ALREADY_EXISTS));
    }
}
