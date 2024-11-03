using Common.Tests.Utilities.Requests.BarberShops;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using FluentAssertions;
using BarberBoss.Exception;

namespace WebApi.Tests.BarberShops.DoLogin;
public class BarberShopDoLoginTests : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "api/barber-shops/signin";
    private readonly HttpClient _httpClient;        

    public BarberShopDoLoginTests(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterBarberShopJsonBuilder.Build();

        await _httpClient.PostAsJsonAsync("api/barber-shops/signup", request);

        var loginRequest = RequestBarberShopDoLoginJsonBuilder.Build();

        loginRequest.Email = request.Email;
        loginRequest.Password = request.Password;

        var result = await _httpClient.PostAsJsonAsync(METHOD, loginRequest);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Email_Invalid_Credentials()
    {
        var request = RequestRegisterBarberShopJsonBuilder.Build();

        await _httpClient.PostAsJsonAsync("api/barber-shops/signup", request);

        var loginRequest = RequestBarberShopDoLoginJsonBuilder.Build();

        loginRequest.Email = string.Empty;
        loginRequest.Password = request.Password;

        var result = await _httpClient.PostAsJsonAsync(METHOD, loginRequest);

        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(errorMessage => errorMessage.GetString()!.Equals(ResourceErrorMessages.INVALID_CREDENTIALS));
    }
}
